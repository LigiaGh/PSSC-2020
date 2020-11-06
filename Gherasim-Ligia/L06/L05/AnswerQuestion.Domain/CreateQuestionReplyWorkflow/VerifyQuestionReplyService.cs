using LanguageExt.Common;
using ReplayQuestion.Domain.CreateQuestionReplyWorkflow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplyQuestion.Domain.CreateQuestionReplyWorkflow
{
    public class VerifyQuestionReplyService
    {
        public Result<VerifiedQuestionReply> VerifyQuestionReplySize(string reply)
        {
            if (IsQuestionResponseValid(reply))
            {
                return new VerifiedQuestionReply(reply);
            }
            else
            {
                return new Result<VerifiedQuestionReply>(new InvalidQuestionResponseException(reply));
            }

        }

        private static bool IsQuestionResponseValid(string reply)
        {
            if (reply.Length <= 1000 && reply.Length >= 10)
            {
                return true;
            }
            return false;
        }
    }
}
