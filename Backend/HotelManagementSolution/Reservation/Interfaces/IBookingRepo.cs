namespace Reservation.Interfaces
{
    public interface IBookingRepo<T,K>
    {
        public Task<T?> Add(T book);
        public Task<T?> Delete(K key);
        public Task<ICollection<T>?> GetByUserId(K key);
        public Task<T?> GetById(K key);
        public Task<ICollection<T>?> GetAll();
    }
}
