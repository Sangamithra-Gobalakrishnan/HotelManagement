namespace UserAuthentication.Interfaces
{
    public interface IUserRepo<T,S,P>
    {
        public Task<T?> Add(T user);
        public Task<T?> Update(T user);
        public Task<T?> GetIdByPhoneNo(P phoneNumber);
        public Task<T?> Get(S id);
        public Task<ICollection<T>?> GetAll();
    }
}
