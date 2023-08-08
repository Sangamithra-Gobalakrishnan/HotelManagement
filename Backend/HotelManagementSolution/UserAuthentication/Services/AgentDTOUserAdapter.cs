using System.Security.Cryptography;
using System.Text;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.DTO;

namespace UserAuthentication.Services
{
    public class AgentDTOUserAdapter : IAgentDTOUserAdapter
    {
        public User GetUserFromAgentDTOAsync(AgentDTO agentDTO)
        {
            if (agentDTO.Password != null)
            {
                var hmac = new HMACSHA512();
                agentDTO.User ??= new User(); // Create a new User object if it's null
                agentDTO.User.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(agentDTO.Password));
                agentDTO.User.PasswordKey = hmac.Key;
                agentDTO.User.Role = "agent";
                agentDTO.User.Status = "not-approved";
                return agentDTO.User;
            }
            else
            {
                throw new Exception("AgentDTO password is null.");
            }
        }
    }
}
