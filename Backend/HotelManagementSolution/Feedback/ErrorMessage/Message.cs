namespace FeedbackAPI.ErrorMessage
{
    public class Message
    {
        public List<string> messages = new List<string>();

        public Message()
        {
            messages = new List<string>()
            {
                "Unable to add feedback right now!",
                "Oops!No feedbacks available with this hotelid",
                "Oops!No feedbacks available with this hotelid for this month"
            };
        }
    }
}
