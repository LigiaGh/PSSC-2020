using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplyQuestion.Domain.CreateQuestionReplyWorkflow
{
    [AsChoice]
    public static partial class CreateReplyResult
    {
        public interface ICreateReplyResult { }

        public class ReplyCreated : ICreateReplyResult
        {
            public Guid QuestionId { get; private set; }
            public string IdOwner { get; private set; }
            public string IdAuthor { get; private set; }
            public string Reply { get; private set; }
            public Guid ReplyId { get; private set; }

            public ReplyCreated(Guid questionId, string idOwner, string idAuthor, string reply, Guid replyId)
            {
                QuestionId = questionId;
                IdOwner = idOwner;
                IdAuthor = idAuthor;
                Reply = reply;
                ReplyId = replyId;
            }
        }

        public class ReplyNotCreated : ICreateReplyResult
        {
            public string Reason { get; set; }

            public ReplyNotCreated(string reason)
            {
                Reason = reason;
            }
        }

        public class ReplyValidationFailed : ICreateReplyResult
        {
            public IEnumerable<string> ValidationErrors { get; private set; }

            public ReplyValidationFailed(IEnumerable<string> errors)
            {
                ValidationErrors = errors.AsEnumerable();
            }
        }

        public class NewQuestionReplyOwnerNotified : ICreateReplyResult
        {
            public Guid QuestionId { get; private set; }
            public string Reply { get; private set; }
            public Guid ReplyId { get; private set; }
            public string OwnerEmail { get; private set; }
            public string IdAuthor { get; private set; }
            public string MessageNotifier { get; private set; }

            public NewQuestionReplyOwnerNotified(Guid questionId, string reply, Guid replyId, string ownerEmail, string idAuthor, string messageNotifier)
            {
                QuestionId = questionId;
                Reply = reply;
                ReplyId = replyId;
                OwnerEmail = ownerEmail;
                IdAuthor = idAuthor;
                MessageNotifier = messageNotifier;
            }
        }

        public class NewQuestionReplyAuthorNotified : ICreateReplyResult
        {
            public Guid QuestionId { get; private set; }
            public string Reply { get; private set; }
            public Guid ReplyId { get; private set; }
            public string AuthorEmail { get; private set; }
            public string IdOwner { get; private set; }
            public string MessageNotifier { get; private set; }

            public NewQuestionReplyAuthorNotified(Guid questionId, string reply, Guid replyId, string authorEmail, string idOwner, string messageNotifier)
            {
                QuestionId = questionId;
                Reply = reply;
                ReplyId = replyId;
                AuthorEmail = authorEmail;
                IdOwner = IdOwner;
                MessageNotifier = messageNotifier;
            }
        }
    }
}
