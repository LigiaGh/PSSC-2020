﻿using Access.Primitives.Extensions.Cloning;
using CSharp.Choices;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.GetQuestionRepyOp
{
    [AsChoice]
    public static partial class GetQuestionReplyResult
    {
        public interface IGetQuestionReplyResult: IDynClonable { }

        public class ReplyReturned: IGetQuestionReplyResult
        {
            public Post Replies { get; }

            public ReplyReturned(Post replies)
            {
                Replies = replies;
            }
            public object Clone() => this.ShallowClone();
        }

        public class RepliesNotReturned : IGetQuestionReplyResult
        {
            public bool OperationCanceled { get; set; }
            public string Reason { get; private set; }


            public RepliesNotReturned(bool operationCanceled, string reason)
            {
                OperationCanceled = operationCanceled;
                Reason = reason;
            }
            public object Clone() => this.ShallowClone();
        }

        public class InvalidRequest : IGetQuestionReplyResult
        {
            //public bool CheckSyntax { get; set; }
            public string Reason { get; private set; }

            public InvalidRequest(string reason)
            {
                Reason = reason;
            }
            public object Clone() => this.ShallowClone();
        }
    }
}
