namespace UserAuthentication.Exceptions
{
    public class UserException:Exception
    {
        string message;
        public UserException(string msg)
        {
            message = msg;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
