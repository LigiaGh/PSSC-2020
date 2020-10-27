using System;
using System.Collections.Generic;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    [Serializable]

    public class InvalidQuestionException : Exception
    {
       
            public InvalidQuestionException()
            {
            }

            public InvalidQuestionException(string question) : base($"\n{question}\n is not valid. The value must be between 30 chars and 1000 chars!")
            {
            }
            public InvalidQuestionException(string[] tags) : base("Invalid Tags!")
            {
            }
            public InvalidQuestionException(string question, string[] tags) : base($"Question and Tags have invalid values.")
            {
            }
    }
}
