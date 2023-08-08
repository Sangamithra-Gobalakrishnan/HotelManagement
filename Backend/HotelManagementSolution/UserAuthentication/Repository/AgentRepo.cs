using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Exceptions;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.Context;

namespace UserAuthentication.Repository
{
    public class AgentRepo : IAgentRepo<TravelAgent, int>
    {
        private readonly UserContext _userContext;

        public AgentRepo(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<TravelAgent?> Add(TravelAgent travelAgent)
        {
            if(_userContext.TravelAgents != null)
            {
                try
                {
                    _userContext.Add(travelAgent);
                    await _userContext.SaveChangesAsync();
                    return travelAgent;
                }
                catch (DbUpdateException)
                {
                    throw new UserException("Error occurred while adding an Agent.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while adding an Agent.");
                }
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }


        public async Task<TravelAgent?> Get(int key)
        {
            if (_userContext.TravelAgents != null)
            {
                var agent = await _userContext.TravelAgents.FirstOrDefaultAsync(a => a.UserId == key);
                if (agent != null)
                    return agent;
                return null;
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }


        public async Task<ICollection<TravelAgent>?> GetAll()
        {
            if (_userContext.TravelAgents != null)
            {
                var agents = await _userContext.TravelAgents.ToListAsync();
                return agents;
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }
    }
}
