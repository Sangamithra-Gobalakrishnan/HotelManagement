namespace UserAuthentication.Interfaces
{
    public interface ITravellerRepo<T,S>
    {
        public Task<T?> Add(T traveller);
        public Task<ICollection<T>?> GetAll();
        public Task<T?> Get(S key);
    }
}
