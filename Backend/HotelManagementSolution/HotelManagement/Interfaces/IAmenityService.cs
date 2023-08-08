namespace HotelManagement.Interfaces
{
    public interface IAmenityService<T,K>
    {
        public Task<T?> Add(T amenityType);
        public Task<T?> Delete(T amenityType);
        public Task<ICollection<T>?> GetAllById(K key);

    }
}
