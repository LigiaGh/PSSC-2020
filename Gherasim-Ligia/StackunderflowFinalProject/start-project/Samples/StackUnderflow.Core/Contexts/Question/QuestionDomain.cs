using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp;
using System;
using System.Collections.Generic;
using System.Text;
using static PortExt;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp.CreateQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public static class QuestionDomain
    {
        public static Port<ICreateQuestionResult> CreateQuestion(CreateQuestionCmd command) => NewPort<CreateQuestionCmd, ICreateQuestionResult>(command);
    }
}
