using LanguageExt;
using ReplayQuestion.Domain.CreateQuestionReplyWorkflow;
using System;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var replyResult = UnverifiedQuestionReply.Create("teeeeeeeeeeeeet.com");


            replyResult.Match(
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
            var verifiedQuestionReplyResult = new VerifyQuestionReplyService().VerifyQuestionSize(reply);
            verifiedQuestionReplyResult.Match(
                    verifiedReply =>
                    {
                        //new RestPasswordService().SendRestPasswordLink(verifiedEmail).Wait();
                        //aici pot trimite notificarile catre owner and so on
                        return Unit.Default;
                    },
                    ex =>
                    {
                        Console.WriteLine("Email address could not be verified");
                        return Unit.Default;
                    }
                );
        }
    }
}
