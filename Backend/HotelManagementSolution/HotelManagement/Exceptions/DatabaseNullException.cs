namespace HotelManagement.Exceptions
{
    public class DatabaseNullException:Exception
    {
        string message;
        public DatabaseNullException(string msg)
        {
            message = msg;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
