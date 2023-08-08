using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.DTO;

namespace UserAuthentication.Services
{
    public class FilterService : IFilterService<Traveller, TravelAgent, StatusDTO>
    {
        private readonly IAgentRepo<TravelAgent, int> _agentRepo;
        private readonly ITravellerRepo<Traveller, int> _travelRepo;
        private readonly IUserRepo<User, int, string> _userRepo;

        public FilterService(IAgentRepo<TravelAgent,int> agentRepo,
                             ITravellerRepo<Traveller,int> travelRepo,
                             IUserRepo<User,int,string> userRepo)
        {
            _agentRepo = agentRepo;
            _travelRepo = travelRepo;
            _userRepo = userRepo;
        }
        public Task<TravelAgent?> GetAgentById(StatusDTO key)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<TravelAgent>?> GetApprovedAgents()
        {
            List<TravelAgent> approvedAgentsList = new List<TravelAgent>();
            var agents = await _agentRepo.GetAll();
            var users = await _userRepo.GetAll();

            var approvedAgents = users?.Where(s => s.Role == "agent" && s.Status == "approved")
                                       .Select(p => p.Id)
                                       .ToList();

            if (approvedAgents != null && approvedAgents.Count > 0)
            {
                foreach (var id in approvedAgents)
                {
                    var agent = agents?.SingleOrDefault(s => s?.UserId == id);
                    if (agent != null)
                        approvedAgentsList.Add(agent);
                }
                return approvedAgentsList;
            }

            return null;
        }

        public async Task<ICollection<Traveller>?> GetAllTravellers()
        {
            var travellers = await _travelRepo.GetAll();
            return travellers?.Count > 0 ? (ICollection<Traveller>?)travellers : null;
        }

        public async Task<ICollection<TravelAgent>?> GetNotApprovedAgents()
        {
            List<TravelAgent> notApprovedAgentsList = new List<TravelAgent>();
            var agents = await _agentRepo.GetAll();
            var users = await _userRepo.GetAll();

            var notApprovedAgents = users?.Where(s => s.Role == "agent" && s.Status == "not-approved")
                                       .Select(p => p.Id)
                                       .ToList();

            if (notApprovedAgents != null && notApprovedAgents.Count > 0)
            {
                foreach (var id in notApprovedAgents)
                {
                    var agent = agents?.SingleOrDefault(s => s?.UserId == id);
                    if (agent != null)
                        notApprovedAgentsList.Add(agent);
                }
                return notApprovedAgentsList;
            }

            return null;
        }

        public async Task<Traveller?> GetTravellerById(StatusDTO key)
        {
            if (key != null)
            {
                var traveller = await _travelRepo.Get(key.AgentID);
                return traveller;
            }

            return null;
        }
    }
}
