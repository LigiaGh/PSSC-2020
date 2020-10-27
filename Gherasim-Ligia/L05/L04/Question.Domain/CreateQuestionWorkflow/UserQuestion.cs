using CSharp.Choices;
using LanguageExt.Common;
using OpenTracing.Tag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    [AsChoice]

    public static partial class UserQuestion
    {
        public interface IUserQuestion { }

        public class UnverifiedQuestion : IUserQuestion
        {
            public string Question { get; private set; } // body 
            public string[] Tags { get; private set; }
            public bool IsPosted { get; set; }

            private UnverifiedQuestion(string question, string[] tags)
            {
                Question = question;
                Tags = tags;
            }

            public static Result<UnverifiedQuestion> Create(string question, string[] tags)
            {
                // nu mai pot crea o intrebare invalida
                if (isQuestionValid(question) && isTagsValid(tags))
                {
                    return new UnverifiedQuestion(question, tags);
                }
                else if (!isQuestionValid(question) && !isTagsValid(tags))
                {
                    return new Result<UnverifiedQuestion>(new InvalidQuestionException(question,tags));
                }
                else if (!isTagsValid(tags))
                {
                    return new Result<UnverifiedQuestion>(new InvalidQuestionException(tags));
                }
                else
                {
                    return new Result<UnverifiedQuestion>(new InvalidQuestionException(question));
                }

            }

            private static bool isQuestionValid(string question)
            {
                if (question.Length > 1000 || question.Length <30)
                    return false;

                return true;
            }
            private static bool isTagsValid(string[] tags)
            {
                if (tags.Length < 1 || tags.Length > 3)
                    return false;

                return true;
            }
        }
        public class VerifiedQuestion : IUserQuestion
        {
            public string Question { get; private set; }
            public string[] Tags { get; private set; }
            internal VerifiedQuestion(string question, string[] tags)
            {
                Question = question;
                Tags = tags;
            }
        }
    }
}
