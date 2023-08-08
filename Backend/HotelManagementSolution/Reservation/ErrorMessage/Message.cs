namespace Reservation.ErrorMessage
{
    public class Message
    {
        public List<string> messages = new List<string>();

        public Message()
        {
            messages = new List<string>()
            {
                "Unable to book room right now!",
                "Unable to cancel room right now!",
                "No booking currently available with this userId"
            };
        }
    }
}
