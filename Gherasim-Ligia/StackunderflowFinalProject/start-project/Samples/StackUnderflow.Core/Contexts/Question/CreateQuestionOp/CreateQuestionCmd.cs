using EarlyPay.Primitives.ValidationAttributes;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp
{
    public struct CreateQuestionCmd
    {
        public CreateQuestionCmd(int questionId, Guid userId, string title, string postText, ICollection<PostTag> postTag)
        {
            PostId = questionId;
            TenantId = userId;
            Title = title;
            PostText = postText;//body
            PostTag = postTag;//tags
        }

        [GuidNotEmpty]
        public int PostId { get; set; }
        [GuidNotEmpty]
        public Guid TenantId { get; set; }

        //[Required]
        //public string UserEmail { get; set; }
        [Required]
        [MinLength(10), MaxLength(300)]
        public string Title { get; set; }
        [Required]
        [MinLength(30), MaxLength(3000)]
        public string PostText { get; set; } //PostText
        [Required]
        public ICollection<PostTag> PostTag { get; set; } //postTag
    }
}

