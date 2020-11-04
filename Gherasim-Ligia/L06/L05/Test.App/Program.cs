using LanguageExt;
using ReplayQuestion.Domain.CreateQuestionReplyWorkflow;
using System;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var replyResult = UnverifiedQuestionReply.Create("tet.com");


            replyResult.Match(
                    Succ: email =>
                    {
                        //SendResetPasswordLink(email);

                        Console.WriteLine($"Your answer is valid");
                        return Unit.Default;
                    },
                    Fail: ex =>
                    {
                        Console.WriteLine($"Invalid email address. Reason: {ex.Message}");
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
