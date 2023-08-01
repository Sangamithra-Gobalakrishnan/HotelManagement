namespace HotelManagement.Exceptions
{
    public class HotelException:Exception
    {
        string message;
        public HotelException(string msg)
        {
            message = msg;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
