namespace HotelManagement.Interfaces
{
    public interface IImageService<T,K>
    {
        public Task<T?> Add(T image);
        public Task<T?> Delete(T image);
        public Task<ICollection<T>?> GetAllById(K key);
    }
}
