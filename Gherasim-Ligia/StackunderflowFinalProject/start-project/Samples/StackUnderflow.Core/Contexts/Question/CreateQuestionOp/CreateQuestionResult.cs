using Access.Primitives.Extensions.Cloning;
using CSharp.Choices;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult : IDynClonable {}

        public class QuestionCreated : ICreateQuestionResult
        {
            public Post Question { get; }

            public QuestionCreated(Post question)
            {
                Question = question;
            }
            public object Clone() => this.ShallowClone();
        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public bool OperationCanceled { get; set; }

            public QuestionNotCreated(bool operationCanceled)
            {
                OperationCanceled = operationCanceled;
            }
            public object Clone() => this.ShallowClone();
        }

        public class InvalidQuestion : ICreateQuestionResult
        {
            //public bool CheckSyntax { get; set; }
            public string Reason { get; private set; }

            public InvalidQuestion(string Reason)
            {
                Reason = Reason;
            }
            public object Clone() => this.ShallowClone();
        }

    }
}
