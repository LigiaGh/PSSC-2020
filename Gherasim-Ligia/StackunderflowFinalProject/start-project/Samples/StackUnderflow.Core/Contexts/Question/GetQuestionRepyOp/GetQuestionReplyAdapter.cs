using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Access.Primitives.IO.Mocking;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Question.GetQuestionRepyOp.GetQuestionReplyResult;

namespace StackUnderflow.Domain.Core.Contexts.Question.GetQuestionRepyOp
{
    public class GetQuestionReplyAdapter: Adapter<GetQuestionReplyCmd, IGetQuestionReplyResult, QuestionWriteContext, QuestionDependencies>
    {
        private readonly IExecutionContext _ex;

        public GetQuestionReplyAdapter(IExecutionContext ex)
        {
            _ex = ex;
        }

        public override async Task<IGetQuestionReplyResult> Work(GetQuestionReplyCmd command, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var workflow = from valid in command.TryValidate()
                           let t = AddReplyIfMissing(state, GetReplyFromCommand(command))
                           select t;


            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new InvalidRequest(ex.ToString()));

            return result;
        }

        public IGetQuestionReplyResult AddReplyIfMissing(QuestionWriteContext state, Post tenant)
        {
            //if (state.Replies.Count(p => p.Name.Equals(tenant.Name)))
                return new RepliesNotReturned(true,"doesn't have any replies");

            //if (state.Tenants.All(p => p.TenantId != tenant.TenantId))
            //    state.Tenants.Add(tenant);
            //return new ReplyReturned(tenant, tenant.TenantUser.Single().User);
        }

        private Post GetReplyFromCommand(GetQuestionReplyCmd cmd)
        {
            var post = new Post()
            {
                PostId = cmd.PostId,
                TenantId = cmd.TenantId,
                PostedBy = cmd.PostedBy,
                PostText = cmd.PostText,
                DateCreated = cmd.DateCreated,
                AcceptedAnswer = cmd.AcceptedAnswer
        };
            return post;
        }

        public override Task PostConditions(GetQuestionReplyCmd op, GetQuestionReplyResult.IGetQuestionReplyResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}
