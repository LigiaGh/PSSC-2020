namespace ReplyQuestion.Domain
{
    internal class InvalidQuestionResponseException : System.Exception
    {
        
        public InvalidQuestionResponseException()
        {
        }

        public InvalidQuestionResponseException(string response) : base($"The response body should be between 10 and 1000 chars.\n Please review your:\n{response}.")
        {
        }
    }
}