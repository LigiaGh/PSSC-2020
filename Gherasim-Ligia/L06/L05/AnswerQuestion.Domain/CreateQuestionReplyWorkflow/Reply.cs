using ReplyQuestion.Domain;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplayQuestion.Domain.CreateQuestionReplyWorkflow
{
    public interface IReply { }

    public class Reply
    {

        public string IdOwner { get; private set; }
        public string OwnerEmail { get; private set; }
        public string IdAuthor { get; private set; }
        public string AuthorEmail { get; private set; }
        public string ReplayBody { get; private set; }
        public DateTime CreatedDate { get; set; }
    }

    public class UnverifiedQuestionReply : IReply
    {
        public string ReplyBody { get; private set; }

        private UnverifiedQuestionReply(string replyBody)
        {
            ReplyBody = replyBody;
        }

        public static Result<UnverifiedQuestionReply> Create(string response)
        {
            if (IsQuestionResponseValid(response))
            {
                return new UnverifiedQuestionReply(response);
            }
            else
            {
                return new Result<UnverifiedQuestionReply>(new InvalidQuestionResponseException(response));
            }
        }

        private static bool IsQuestionResponseValid(string response)
        {
            if (response.Length <= 1000 && response.Length >= 10)
            {
                return true;
            }
            return false;
        }
    }

    public class VerifiedQuestionResult : IReply
    {
        public string ReplyTextBody { get; private set; }

        internal VerifiedQuestionResult(string replyTextBody)
        {
            ReplyTextBody = replyTextBody;
        }
    }
}
