using LanguageExt;
using Question.Domain;
using Question.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using System.Net;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult.QuestionCreated;
using static Question.Domain.CreateQuestionWorkflow.UserQuestion;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {

            //var question = AskAQuestion.Create("somet");
            var questionResult = UnverifiedQuestion.Create(":)", new string[] { "C#", "WorkflowCode", "Visual Studio" });

            questionResult.Match(
                Succ: question =>
                {
                    SendQuestionForChecking(question);
                    Console.WriteLine("Question is valid");
                    return Unit.Default;
                },
                Fail: ex =>
                {
                    Console.WriteLine($"Invalid question. Reason:{ex.Message}");
                    return Unit.Default;
                }
            );

            Console.ReadLine();
        }

        private static void SendQuestionForChecking(UnverifiedQuestion question)
        {
            var verifiedQuestionResult = new VerifyQuestionService().VerifyQuestion(question);
            verifiedQuestionResult.Match(
                    verifiedQuestion =>
                    {
                        new MLApproversCheckService().SendQuestionForChecking(verifiedQuestion).Wait();
                        return Unit.Default;
                    },
                    ex =>
                    {
                        Console.WriteLine("Question is not valid!");
                        return Unit.Default;
                    }
                );
        }
    }
}
