using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionWriteContext
    {
        //operatii de scriere
        //primesc colectii asupra carora lucrez
        public ICollection<Post> Questions {get;}
        public ICollection<User> Users { get; }
        public ICollection<Post> Replies { get; }

        public QuestionWriteContext(ICollection<Post> questions, ICollection<User> users)
        {
            Questions = questions ?? new List<Post>(0);
            Users = users ?? new List<User>(0);
            Replies = Replies ?? new List<Post>(0);
        }

        //public QuestionWriteContext(ICollection<Post> questions, ICollection<User> users, ICollection<PostTag> tags)
        //{
        //    Questions = questions ?? new List<Post>(0);
        //    Users = users ?? new List<User>(0);
        //    Tags = tags ?? new List<PostTag>(0);
        //}

    }
}
