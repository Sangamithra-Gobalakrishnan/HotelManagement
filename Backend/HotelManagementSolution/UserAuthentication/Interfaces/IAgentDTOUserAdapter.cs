using UserAuthentication.Models.DTO;
using UserAuthentication.Models;

namespace UserAuthentication.Interfaces
{
    public interface IAgentDTOUserAdapter
    {
        public User GetUserFromAgentDTOAsync(AgentDTO agentDTO);
    }
}
