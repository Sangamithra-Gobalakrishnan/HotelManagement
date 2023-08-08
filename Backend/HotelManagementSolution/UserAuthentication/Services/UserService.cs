using System.Security.Cryptography;
using System.Text;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.DTO;

namespace UserAuthentication.Services
{
    public class UserService : IUserService<UserDTO, TravellerDTO, AgentDTO, StatusDTO>
    {
        private readonly IAgentRepo<TravelAgent, int> _agentRepo;
        private readonly ITravellerRepo<Traveller, int> _travellerRepo;
        private readonly IAgentDTOUserAdapter _agentDTOUserAdapter;
        private readonly ITravellerDTOUserAdapter _travellerDTOUserAdapter;
        private readonly IUserRepo<User, int, string> _userRepo;
        private readonly IGenerateToken<UserDTO> _generateToken;

        public UserService(IAgentRepo<TravelAgent,int> agentRepo,
                           ITravellerRepo<Traveller,int> travellerRepo,
                           IAgentDTOUserAdapter agentDTOUserAdapter,
                           ITravellerDTOUserAdapter travellerDTOUserAdapter,
                           IUserRepo<User,int,string> userRepo,
                           IGenerateToken<UserDTO> generateToken) 
        { 
            _agentRepo = agentRepo;
            _travellerRepo = travellerRepo;
            _agentDTOUserAdapter = agentDTOUserAdapter;
            _travellerDTOUserAdapter = travellerDTOUserAdapter;
            _userRepo = userRepo;
            _generateToken = generateToken;
        }

        public async Task<UserDTO?> AgentRegister(AgentDTO agentRegister)
        {
            UserDTO? login = null;
            var userNew = _agentDTOUserAdapter.GetUserFromAgentDTOAsync(agentRegister);
            var userResult = await _userRepo.Add(userNew);
            agentRegister.Age = DateTime.Today.Year - new DateTime(agentRegister.DateOfBirth.Year, agentRegister.DateOfBirth.Month, agentRegister.DateOfBirth.Day).Year;
            var agentResult = await _agentRepo.Add(agentRegister);
            if (userResult != null && agentResult != null)
            {
                login = new UserDTO();
                login.UserId = userResult.Id;
                login.Role = userResult.Role;
                login.Status = userResult.Status;
                login.Token = _generateToken.GenerateToken(login);
            }
            return login;
        }

        public async Task<StatusDTO?> ApproveAgent(StatusDTO status)
        {
            if (status != null)
            {
                var agent = await _userRepo.Get(status.AgentID);
                if (agent != null)
                {
                    if (agent.Status == "approved")
                        agent.Status = "not-approved";
                    else
                        agent.Status = "approved";
                    var result = await _userRepo.Update(agent);
                    if (result != null)
                    {
                        return status;
                    }
                }
                return null;
            }
            return null;
        }

        public async Task<UserDTO?> Login(UserDTO loginDTO)
        {
            UserDTO? login = null;
            var userData = await _userRepo.GetIdByPhoneNo(loginDTO?.PhoneNumber ?? "");
            if (userData != null && loginDTO?.Password != null && userData.PasswordKey != null && userData.PasswordHash != null)
            {
                var hmac = new HMACSHA512(userData.PasswordKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.PasswordHash[i])
                        return null;
                }
                login = new UserDTO();
                login.UserId = userData.Id;
                login.Role = userData.Role;
                login.Status = userData.Status;
                login.Token = _generateToken.GenerateToken(login);
            }
            return login;
        }

        public async Task<UserDTO?> TravellerRegister(TravellerDTO travellerregister)
        {
            UserDTO? login = null;
            var userNew = _travellerDTOUserAdapter.GetUserFromTravellerDTOAsync(travellerregister);
            var userResult = await _userRepo.Add(userNew);
            travellerregister.Age = DateTime.Today.Year - new DateTime(travellerregister.DateOfBirth.Year, travellerregister.DateOfBirth.Month, travellerregister.DateOfBirth.Day).Year;
            var travellerResult = await _travellerRepo.Add(travellerregister);
            if (userResult != null && travellerResult != null)
            {
                login = new UserDTO();
                login.UserId = userResult.Id;
                login.Role = userResult.Role;
                login.Status = userResult.Status;
                login.Token = _generateToken.GenerateToken(login);
            }
            return login;
        }

    }
}
