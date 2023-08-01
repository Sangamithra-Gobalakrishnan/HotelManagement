namespace HotelManagement.ErrorMessage
{
    public class Message
    {
        public List<string> messages = new List<string>();

        public Message()
        {
            messages = new List<string>()
            {
                "Unable to add hotel information right now!",
                "Unable to update hotel information!",
                "Unable to delete hotel information!",
                "Oops!No hotels available right this moment",
                "Oops!No hotels available with this id"
            };
        }
    }
}
