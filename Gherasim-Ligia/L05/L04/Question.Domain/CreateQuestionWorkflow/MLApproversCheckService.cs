using LanguageExt.Common;
using OpenTracing.Tag;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Question.Domain.CreateQuestionWorkflow.UserQuestion;

namespace Question.Domain.CreateQuestionWorkflow
{
    public class MLApproversCheckService
    {
        public Task SendQuestionForChecking(VerifiedQuestion question)
        {
            //ensure the question was verified by MLApprovers amd posted onthe  user workflow
            //MLApproversCheck checkQuestion = checkQuestion(question);

            try
            {
                isQuestionValid(question.Question, question.Tags);
                //if(isQuestionValid(question.Question, question.Tags))
                //set IsPosted on true
                //else set isposted in false and display the MLApprovers feedback
            }
            catch
            {
            }
            //invoke the send logic
            return Task.CompletedTask;
        }

        private bool isQuestionValid(string question, string[] tags)
        {
            if (question.Contains(":)"))
                return false;

            return true;
        }

        //private class MLApproversCheck
        //{

        //}
    }

    
}
