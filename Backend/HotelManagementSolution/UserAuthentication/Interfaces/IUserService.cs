namespace UserAuthentication.Interfaces
{
    public interface IUserService<T,R,A,S>
    {
        public Task<T?> Login(T login);
        public Task<T?> TravellerRegister(R travellerregister);
        public Task<T?> AgentRegister(A agentRegister);
        public Task<S?> ApproveAgent(S status);
    }
}
