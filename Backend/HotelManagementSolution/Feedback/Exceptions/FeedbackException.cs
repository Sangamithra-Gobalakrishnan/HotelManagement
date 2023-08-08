namespace FeedbackAPI.Exceptions
{
    public class FeedbackException : Exception
    {
        string message;
        public FeedbackException(string msg)
        {
            message = msg;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
