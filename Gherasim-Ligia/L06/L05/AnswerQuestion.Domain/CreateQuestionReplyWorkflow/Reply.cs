using ReplyQuestion.Domain;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReplayQuestion.Domain.CreateQuestionReplyWorkflow
{
    public interface IReply { }

    public class Reply : IReply
    {

        public string IdOwner { get; private set; }
        public string OwnerEmail { get; private set; }

        [Required]
        public string IdAuthor { get; private set; }
        public string AuthorEmail { get; private set; }
        [Required]
        public string ReplyBody { get; private set; }
        public DateTime CreatedDate { get; set; }

        public Reply(string idOwner, string ownerEmail, string idAuthor, string authorEmail, string replyBody, DateTime createdDate)
        {
            IdOwner = idOwner;
            OwnerEmail = ownerEmail;
            IdAuthor = idAuthor;
            AuthorEmail = authorEmail;
            ReplyBody = replyBody;
            CreatedDate = createdDate;
        }
    }

    public class UnverifiedQuestionReply : IReply
    {
        public string ReplyBody { get; private set; }

        private UnverifiedQuestionReply(string replyBody)
        {
            ReplyBody = replyBody;
        }

        //doar proprietati si factory 
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

        private static bool IsQuestionResponseValid(string reply)
        {
            if (reply.Length <= 1000 && reply.Length >= 10)
            {
                return true;
            }
            return false;
        }
    }

    public class VerifiedQuestionReply : IReply
    {
        public string ReplyTextBody { get; private set; }

        internal VerifiedQuestionReply(string replyTextBody)
        {
            ReplyTextBody = replyTextBody;
        }
    }
    public class SendAckToOwner : IReply
    {

    }

    public class PublishQuestionReply: IReply
    {

    }
    //public class LanguageCheckService
    //{

    //    public Task DoLanguageCheckService(VerifiedQuestionReply reply)
    //    {
    //        //if (reply.ReplyTextBody.Contains("^"))
    //        //    return Task.;
    //        //invoke the send logic

    //        return Task.CompletedTask;
    //    }
    //}
}
