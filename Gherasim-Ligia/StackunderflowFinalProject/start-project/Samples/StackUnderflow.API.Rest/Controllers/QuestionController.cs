using Access.Primitives.EFCore;
using Access.Primitives.IO;
using GrainInterfaces;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp;
using StackUnderflow.Domain.Core.Contexts.Question.GetQuestionRepyOp;
using StackUnderflow.Domain.Core.Contexts.Question.SendUserConfirmationOp;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackUnderflow.API.AspNetCore.Controllers
{

    [ApiController]
    [Route("question")]
    public class QuestionController : Controller
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;
        private readonly IClusterClient _client; // injectam un client pe care il putem folosi ca sa ne conectam la clusterul nostru

        public QuestionController(IInterpreterAsync interpreter, StackUnderflowContext dbContext, IClusterClient client)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
            _client = client;
        }

        //[HttpGet("getquestion")]
        //public async Task<IActionResult> CreateQuestionAsyncAndSendEmail(int questionId)
        //{
        //    //get ref to question gri
        //    //grain.GetQuestion
        //}

        [HttpPost("postquestion")]
        public async Task<IActionResult> CreateQuestionAsyncAndSendEmail([FromBody] CreateQuestionCmd createQuestionCmd)
        {
            QuestionWriteContext ctx = new QuestionWriteContext(
                new EFList<Post>(_dbContext.Post),
                new EFList<User>(_dbContext.User));

            var dependencies = new QuestionDependencies();
            dependencies.SendConfirmationEmail = (ConfirmationLetter letter) => async () => new ConfirmationAcknowledgement(Guid.NewGuid().ToString());
            dependencies.SendConfirmationEmail = SendEmail;

            var expr = from createQuestionResult in QuestionDomain.CreateQuestion(createQuestionCmd)
                       select createQuestionResult;

            var r = await _interpreter.Interpret(expr, ctx, dependencies);
            _dbContext.SaveChanges();
            return r.Match(
                created => Ok(created.Question.PostId),
                 notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Question could not be created."),//todo return 500 (),
            invalidRequest => BadRequest("Invalid request."));
        }

        private TryAsync<ConfirmationAcknowledgement> SendEmail(ConfirmationLetter letter)
        => async () =>
        {
            var emailSender = _client.GetGrain<IEmailQuestionSender>(Guid.NewGuid());
            await emailSender.SendConfirmationEmailAsync(letter.Letter);

            var guid = Guid.Empty;
            var streamProvider = _client.GetStreamProvider("SMSProvider");
            var stream = streamProvider.GetStream<string>(guid, "LETTER");
            await stream.OnNextAsync("Hello event");
            return new ConfirmationAcknowledgement(Guid.NewGuid().ToString());
        };

        [HttpPost("question/{questionId}")]
        public async Task<IActionResult> CreateReply(int questionId, [FromBody] GetQuestionReplyCmd getQuestionReplyCmd)
        {
            // o noua comanda, o operatiune de comanda si din ea sa trimitem un mesaj prin streamul de eventuri ca sa ne dam seama ca s-a create un nou reply
            // trimitem un mesaj printr-un stream catre un grain
            // id-ul grainului este id-ul intrebarii

            QuestionWriteContext ctx = new QuestionWriteContext(
                new EFList<Post>(_dbContext.Post),
                new EFList<User>(_dbContext.User));

            var dependencies = new QuestionDependencies();

            var expr = from createQuestionResult in QuestionDomain.GetQuestionReply(getQuestionReplyCmd)
                       select createQuestionResult;

            var r = await _interpreter.Interpret(expr, ctx, dependencies);
            _dbContext.SaveChanges();
            return r.Match(
                created => Ok(created.Replies.PostId),
                 notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Question could not be created."),//todo return 500 (),
            invalidRequest => BadRequest("Invalid request."));
        }
    }
}

