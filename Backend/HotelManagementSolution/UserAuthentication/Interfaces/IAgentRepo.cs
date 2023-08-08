namespace UserAuthentication.Interfaces
{
    public interface IAgentRepo<T,S>
    {
        public Task<T?> Add(T agent);
        public Task<ICollection<T>?> GetAll();
        public Task<T?> Get(S key);
    }
}
