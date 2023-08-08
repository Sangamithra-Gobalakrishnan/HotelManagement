namespace UserAuthentication.Exceptions
{
    public class SqlDatabaseException:Exception
    {
        string message;
        public SqlDatabaseException(string msg)
        {
            message = msg;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
