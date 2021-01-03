using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Access.Primitives.IO.Mocking;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp.CreateQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp
{
    public class CreateQuestionAdapter: Adapter<CreateQuestionCmd, ICreateQuestionResult, QuestionWriteContext, QuestionDependencies>
    {
        private readonly IExecutionContext _ex;

        public CreateQuestionAdapter(IExecutionContext ex)
        {
            _ex = ex;
        }

        public override async Task<ICreateQuestionResult> Work(CreateQuestionCmd command, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var workflow = from valid in command.TryValidate()
                           let t = AddQuestionIfMissing(state, CreateQuestionFromCommand(command))
                           select t;


            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new InvalidRequest(ex.ToString()));

            return result;
        }

        public ICreateQuestionResult AddQuestionIfMissing(QuestionWriteContext state, Post post)
        {
            if (state.Questions.Any(p => p.Title.Equals(post.Title)))
                return new QuestionNotCreated(true,"already exists");

            if (state.Questions.All(p => p.PostId != post.PostId))
                state.Questions.Add(post);
            return new QuestionCreated(post);
        }

        private Post CreateQuestionFromCommand(CreateQuestionCmd cmd)
        {
            var post = new Post()
            {
                PostId = cmd.PostId,
                    TenantId = cmd.TenantId,
                    Title = cmd.Title,
                    PostText = cmd.PostText
                    //PostTag = cmd.PostTag,                
            };
            return post;
        }
        public override Task PostConditions(CreateQuestionCmd op, ICreateQuestionResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

    }
}
