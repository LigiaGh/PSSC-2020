using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp;
using StackUnderflow.Domain.Core.Contexts.Question.SendUserConfirmationOp;
using System;
using System.Collections.Generic;
using System.Text;
using static PortExt;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp.CreateQuestionResult;
using static StackUnderflow.Domain.Core.Contexts.Question.SendUserConfirmationOp.SendConfirmationResult;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public static class QuestionDomain
    {
        public static Port<ICreateQuestionResult> CreateQuestion(CreateQuestionCmd command) => NewPort<CreateQuestionCmd, ICreateQuestionResult>(command);
        public static Port<ISendConfirmationResult> CreateQuestionConfirmation (SendConfirmationCmd command) => NewPort<SendConfirmationCmd, ISendConfirmationResult>(command);

    }
}
