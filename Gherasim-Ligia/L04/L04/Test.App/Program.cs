using Question.Domain;
using System;
using System.Collections.Generic;
using System.Net;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult.QuestionCreated;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {

            var cmd = new CreateQuestionCmd("How to create a custom attribute in C#"
                , "I have tried lots of times but still I am not able to understand the usage of custom attributes (I have already gone through lots of links)."
                + "\nCan anyone please explain to me a very basic example of a custom attribute with code ? "
                , new string[] { "C#", "ComponentModel", "Annotations" });

            var result = CreateQuestion(cmd);

            result.Match(
                ProcessQuestionCreated,
                ProcessQuestionNotCreated,
                ProcessInvalidQuestion
            );

            Console.ReadLine();
        }

        private static ICreateQuestionResult ProcessQuestionNotCreated(QuestionNotCreated questionNotCreatedResult)
        {
            Console.WriteLine($"Question not created: {questionNotCreatedResult.Feedback}");
            return questionNotCreatedResult;
        }

        private static ICreateQuestionResult ProcessQuestionCreated(QuestionCreated question)
        {
            Console.WriteLine($"Question {question.QuestionId}");
            return question;
        }

        private static ICreateQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }

        public static ICreateQuestionResult CreateQuestion(CreateQuestionCmd createQuestionCommand)
        {
            if (string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Title is missing" };
                return new QuestionValidationFailed(errors);
            }

            if(createQuestionCommand.Title.Length < 15 && !string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Title cannot be shorter than 15 characters." };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Title.Length > 150)
            {
                var errors = new List<string>() { "Title cannot be longer than 150 characters." };
                return new QuestionValidationFailed(errors);
            }

            if (string.IsNullOrWhiteSpace(createQuestionCommand.Body))
            {
                var errors = new List<string>() { "Body is missing" };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Body.Length < 30 && !string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Body cannot be shorter than 30 characters." };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Body.Length > 30000)
            {
                var errors = new List<string>() { "Body is limited to 30000 characters; you entered 100124." };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Tags.Length < 1)
            {
                var errors = new List<string>() { "Please enter at least one tag; see a list of popular tags." };
                return new QuestionValidationFailed(errors);
            }

            if (new Random().Next(10) > 1)
            {
                return new QuestionNotCreated("Question could not be verified");
            }

            var questionId = Guid.NewGuid();
            var result = new QuestionCreated(questionId, createQuestionCommand.Title, "ligia.gherasim@gmail.com",true);

            if (result.MLApproved)
            {
                result.SetQuestionProfileCount();
                Console.WriteLine(result.QuestionProfileNumber);
            }
            else
            {
                Console.WriteLine(result.QuestionProfileNumber);
                QuestionNotCreated feedback = new QuestionNotCreated("Question closed. Please edit it or delete it.");
            }

            //execute logic
            return result;
        }
    }
}
