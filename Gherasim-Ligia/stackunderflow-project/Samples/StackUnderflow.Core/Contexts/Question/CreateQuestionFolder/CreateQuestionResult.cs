using Access.Primitives.Extensions.Cloning;
using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionFolder
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult: IDynClonable { }

        public class CreateQuestion : ICreateQuestionResult
        {
            public object Clone()
            {
                throw new NotImplementedException();
            }
        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public bool OperationCanceled { get; set; }
            public object Clone()
            {
                throw new NotImplementedException();
            }
        }

        public class InvalidQuestion : ICreateQuestionResult
        {
            public bool CheckSyntax { get; set; }
            public string Reason { get; set; }
            public object Clone()
            {
                throw new NotImplementedException();
            }
        }

    }
}
