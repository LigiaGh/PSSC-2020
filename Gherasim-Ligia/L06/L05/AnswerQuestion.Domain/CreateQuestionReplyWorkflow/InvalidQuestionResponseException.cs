namespace ReplyQuestion.Domain
{
    internal class InvalidQuestionResponseException : System.Exception
    {
        
        public InvalidQuestionResponseException()
        {
        }

        public InvalidQuestionResponseException(string reply) : base($"The reply body should be between 10 and 1000 chars.\n Please review your solution:\n{reply}.")
        {
        }
    }
}