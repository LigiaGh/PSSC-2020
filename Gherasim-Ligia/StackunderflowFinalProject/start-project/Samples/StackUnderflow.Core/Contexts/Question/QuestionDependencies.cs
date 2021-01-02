using LanguageExt;
using StackUnderflow.Domain.Schema.Backoffice.InviteTenantAdminOp;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionDependencies
    {
        public Func<InvitationLetter, TryAsync<InvitationAcknowledgement>> SendInvitationEmail { get; set; }
    }
}
