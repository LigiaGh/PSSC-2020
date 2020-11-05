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
                        //SendResetPasswordLink(email);

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

        //private static void SendResetPasswordLink(UnverifiedEmail email)
        //{
        //    var verifiedEmailResult = new VerifyEmailService().VerifyEmail(email);
        //    verifiedEmailResult.Match(
        //            verifiedEmail =>
        //            {
        //                new RestPasswordService().SendRestPasswordLink(verifiedEmail).Wait();
        //                return Unit.Default;
        //            },
        //            ex =>
        //            {
        //                Console.WriteLine("Email address could not be verified");
        //                return Unit.Default;
        //            }
        //        );
        //}
    }
}
