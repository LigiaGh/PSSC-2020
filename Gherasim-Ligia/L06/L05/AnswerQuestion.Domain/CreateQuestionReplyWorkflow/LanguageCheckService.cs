using ReplayQuestion.Domain.CreateQuestionReplyWorkflow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplyQuestion.Domain.CreateQuestionReplyWorkflow
{
    class LanguageCheckService
    {
        public bool DoLanguageCheckService(VerifiedQuestionReply reply)
        {
            if (reply.ReplyTextBody.Contains("^"))
                return false;

            return true;
        }
    }
}
