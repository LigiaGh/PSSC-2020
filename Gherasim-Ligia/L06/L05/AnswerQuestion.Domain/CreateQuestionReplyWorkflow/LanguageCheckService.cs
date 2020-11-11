using LanguageExt.Common;
using ReplayQuestion.Domain.CreateQuestionReplyWorkflow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplyQuestion.Domain.CreateQuestionReplyWorkflow
{
    class LanguageCheckService
    {
        public static Result<VerifiedQuestionReply> DoLanguageCheckService(UnverifiedQuestionReply reply)
        {
            if (IsLanguageValid(reply))
            {
                return new VerifiedQuestionReply(reply);
            }
            else
            {
                return new Result<VerifiedQuestionReply>(new InvalidQuestionResponseException(reply.ReplyBody,"wrong"));
            }
        }

        public static bool IsLanguageValid(UnverifiedQuestionReply reply)
        {
            if (reply.ReplyBody.Contains("^"))
                return false;

            return true;
        }
    }
}
