using LanguageExt;
using ReplayQuestion.Domain.CreateQuestionReplyWorkflow;
using ReplyQuestion.Domain.CreateQuestionReplyWorkflow;
using System;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //var replyInfo = new Reply("ownerUser", "ownerUser@test.te", "authorUser", "authorUser@test.te", "Please write down your answer.", DateTime.Now);
            var replyInfo = new Reply("ownerUser", "ownerUser@test.te", "authorUser", "authorUser@test.te", "Pleas", DateTime.Now);
            var check = UnverifiedQuestionReply.Create(replyInfo.ReplyBody);


            check.Match(
                    Succ: reply =>
                    {
                        VerifyLanguageCheck(reply);
                        Console.WriteLine($"The reply body \n{reply}\npassed the length check");
                        return Unit.Default;
                    },
                    Fail: ex =>
                    {
                        Console.WriteLine($"{ex.Message}");
                        return Unit.Default;
                    }
                );
            

            Console.ReadLine();
        }

        private static void VerifyLanguageCheck(UnverifiedQuestionReply reply)
        {
            var verifiedQuestionReplyResult = new VerifyQuestionReplyService();
            verifiedQuestionReplyResult.VerifyQuestionReplyLength(reply).Match(
                    verifiedReply =>
                    {
                        //
                        //aici pot trimite notificarile catre owner su author
                        return Unit.Default;
                    },
                    ex =>
                    {
                        Console.WriteLine($"{ex.Message}");
                        return Unit.Default;
                    }
                );
        }
    }
}
