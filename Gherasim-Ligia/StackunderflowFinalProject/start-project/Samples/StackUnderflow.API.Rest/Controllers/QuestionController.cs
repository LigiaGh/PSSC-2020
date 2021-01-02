using Access.Primitives.EFCore;
using Access.Primitives.IO;
using GrainInterfaces;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp;
using StackUnderflow.Domain.Schema.Backoffice.InviteTenantAdminOp;
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
            private readonly IClusterClient _client;

            public QuestionController(IInterpreterAsync interpreter, StackUnderflowContext dbContext, IClusterClient client)
            {
                _interpreter = interpreter;
                _dbContext = dbContext;
                _client = client;
            }

            [HttpPost("post")]
            public async Task<IActionResult> CreateQuestionAsync([FromBody] CreateQuestionCmd createQuestionCmd)
            {
            QuestionWriteContext ctx = new QuestionWriteContext(
                new EFList<Post>(_dbContext.Post));

            var dependencies = new QuestionDependencies();
            dependencies.SendInvitationEmail = SendEmail;

            var expr = from createQuestionResult in QuestionDomain.CreateQuestion(createQuestionCmd)
                           select new { createQuestionResult};

                var r = await _interpreter.Interpret(expr, ctx, dependencies);
                _dbContext.SaveChanges();
                return r.createQuestionResult.Match(
                    created => (IActionResult)Ok(created.Question.PostId),
                    notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Question could not be created."),//todo return 500 (),
                invalidRequest => BadRequest("Invalid request."));
            }

        private TryAsync<InvitationAcknowledgement> SendEmail(InvitationLetter letter)
        => async () =>
        {
            var emialSender = _client.GetGrain<IEmailSender>(0);
            await emialSender.SendEmailAsync(letter.Letter);
            return new InvitationAcknowledgement(Guid.NewGuid().ToString());
        };
    }
    }

