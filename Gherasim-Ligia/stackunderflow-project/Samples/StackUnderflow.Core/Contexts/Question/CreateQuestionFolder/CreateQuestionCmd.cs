using EarlyPay.Primitives.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionFolder
{
    public struct CreateQuestionCmd
    {
        public CreateQuestionCmd(Guid questionId, Guid userId, string userEmail, string title, string body, string tags)
        {
            QuestionId = questionId;
            UserId = userId;
            UserEmail = userEmail;
            Title = title;
            Body = body;
            Tags = tags;
        }

        [GuidNotEmpty]
        public Guid QuestionId { get; set; }
        [GuidNotEmpty]
        public Guid UserId { get; set; }

        [Required]
        public string UserEmail { get; set; }
        [Required]
        [MinLength(10),MaxLength(300)]
        public string Title { get; set; }
        [Required]
        [MinLength(30), MaxLength(3000)]
        public string Body { get; set; }
        [Required]
        public string Tags { get; set; }
    }
}
