namespace HotelManagement.Exceptions
{
    public class RoomNotFoundException:Exception
    {
        string message;
        public RoomNotFoundException(string msg)
        {
            message = msg;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
