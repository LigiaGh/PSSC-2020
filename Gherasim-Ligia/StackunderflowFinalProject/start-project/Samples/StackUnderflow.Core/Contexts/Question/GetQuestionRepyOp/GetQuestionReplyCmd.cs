using EarlyPay.Primitives.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.GetQuestionRepyOp
{
    public class GetQuestionReplyCmd
    {
        public GetQuestionReplyCmd(int questionId, int userId, Guid postedby, string postText, DateTime dateCreated, bool acceptedAnswer)
        {
            PostId = questionId;
            TenantId = userId;
            PostedBy = postedby;
            PostText = postText;//body
            DateCreated = dateCreated;
            AcceptedAnswer = acceptedAnswer;
        }

        [Required]
        public int PostId { get; set; }
        [Required]
        public int TenantId { get; set; }

        [GuidNotEmpty]
        public Guid PostedBy { get; set; }

        //[Required]
        //public string UserEmail { get; set; }
        [Required]
        public bool AcceptedAnswer { get; set; }

        [GuidNotEmpty]
        public DateTime DateCreated { get; set; }

        [Required]
        [MinLength(30), MaxLength(3000)]
        public string PostText { get; set; }
        //[Required]
        // public string PostTag { get; set; } 
    }
}

