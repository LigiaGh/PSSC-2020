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
        [Required]
        public string IdOwner { get; private set; }
        [Required]
        public string OwnerEmail { get; private set; }

        [Required]
        public string IdAuthor { get; private set; }
        [Required]
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

        //doar proprietati si factory method
        public static Result<UnverifiedQuestionReply> Create(string response)
        {
                return new UnverifiedQuestionReply(response);
        }
    }

    public class VerifiedQuestionReply : IReply
    {
        public string ReplyTextBody { get; private set; }

        internal VerifiedQuestionReply(UnverifiedQuestionReply replyTextBody)
        {
            ReplyTextBody = replyTextBody.ReplyBody;
        }
    }
    public class SendAckToOwner : IReply
    {
        public string OwnerEmail { get; private set; }
        public string DefaultNewReplyMessage { get; private set; }
        public string IdAuthor { get; private set; }
        public SendAckToOwner( string ownerEmail, string message)
        {
            OwnerEmail = ownerEmail;
            DefaultNewReplyMessage = message;
        }

    }

    public class PublishQuestionReply : IReply
    {
        public string AuthorEmail { get; private set; }
        public string Message { get; private set; }
        public Guid IdQuestion { get; private set; }
        public bool IsPosted { get; private set; }

        public PublishQuestionReply(string authorEmail, string message, Guid idQuestion, bool isPosted)
        {
            AuthorEmail = authorEmail;
            Message = message;
            IdQuestion = idQuestion;
            IsPosted = isPosted;
        }

    }

}
