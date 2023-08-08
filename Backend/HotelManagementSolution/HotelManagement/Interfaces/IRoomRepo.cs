namespace HotelManagement.Interfaces
{
    public interface IRoomRepo<T,D,K>
    {
        public Task<T?> Add(T room);
        public Task<T?> Update(T room);
        public Task<D?> Delete(D room);
        public Task<ICollection<T>?> GetAllById(K key);
    }
}
