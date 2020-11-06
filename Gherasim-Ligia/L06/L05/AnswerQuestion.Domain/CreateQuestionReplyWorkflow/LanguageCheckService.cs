using ReplayQuestion.Domain.CreateQuestionReplyWorkflow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReplyQuestion.Domain.CreateQuestionReplyWorkflow
{
    class LanguageCheckService
    {
        public Task DoLanguageCheckService(VerifiedQuestionReply reply)
        {
            //if (reply.ReplyTextBody.Contains("^"))
            //    return Task.;
            //invoke the send logic

            return Task.CompletedTask;
        }
    }
}
