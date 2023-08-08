namespace HotelManagement.Interfaces
{
    public interface IHotelRepo<T,K>
    {
        public Task<T?> Add(T hotel);
        public Task<T?> Update(T hotel);
        public Task<T?> Delete(K key);
        public Task<T?> GetById(K key);
        public Task<ICollection<T?>> GetByAgentId(K key);
        public Task<ICollection<T>?> GetAll();
    }
}
