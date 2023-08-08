
namespace Reservation.Interfaces
{
    public interface IBookingService<T,K>
    {
        public Task<T?> Add(T book);
        public Task<T?> Delete(K key);
        public Task<ICollection<T>?> GetByUserId(K key);
    }
}
