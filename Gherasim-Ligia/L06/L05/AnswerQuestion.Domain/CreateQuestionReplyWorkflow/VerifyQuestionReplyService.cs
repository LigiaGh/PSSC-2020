using LanguageExt.Common;
using ReplayQuestion.Domain.CreateQuestionReplyWorkflow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplyQuestion.Domain.CreateQuestionReplyWorkflow
{
    public class VerifyQuestionReplyService
    {
        public Result<VerifiedQuestionReply> VerifyQuestionReplyLength(UnverifiedQuestionReply replyBody)
        {
            if (IsQuestionResponseValid(replyBody.ReplyBody))
            {
                return new VerifiedQuestionReply(replyBody);
            }
            else
            {
                return new Result<VerifiedQuestionReply>(new InvalidQuestionResponseException(replyBody.ReplyBody));
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
