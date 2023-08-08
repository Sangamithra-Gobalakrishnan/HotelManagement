namespace UserAuthentication.Interfaces
{
    public interface IFilterService<T,A,I>
    {
        public Task<ICollection<T>?> GetAllTravellers();
        public Task<ICollection<A>?> GetApprovedAgents();
        public Task<ICollection<A>?> GetNotApprovedAgents();
        public Task<A?> GetAgentById(I key);
        public Task<T?> GetTravellerById(I key);
    }
}
