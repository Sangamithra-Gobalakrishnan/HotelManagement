namespace Feedback.Interfaces
{
    public interface IFeedbackService<T,K>
    {
        public Task<T?> Add(T feedback);
        public Task<ICollection<T>?> GetByHotelId(K hotelId);
        public Task<ICollection<T>?> GetFeedbackByMonth(K month);
    }
}
