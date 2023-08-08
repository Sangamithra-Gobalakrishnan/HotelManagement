namespace Feedback.Models.DTO
{
    public class FeedbackDTO:HotelFeedback
    {
        public ICollection<HotelFeedback>? HotelFeedbacks { get; set; }
    }
}
