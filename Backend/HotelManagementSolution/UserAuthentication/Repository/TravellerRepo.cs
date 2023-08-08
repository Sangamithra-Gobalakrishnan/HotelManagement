using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Exceptions;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.Context;

namespace UserAuthentication.Repository
{
    public class TravellerRepo : ITravellerRepo<Traveller, int>
    {
        private readonly UserContext _userContext;

        public TravellerRepo(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<Traveller?> Add(Traveller traveller)
        {
            if (_userContext.Travellers != null)
            {
                try
                {
                    _userContext.Add(traveller);
                    await _userContext.SaveChangesAsync();
                    return traveller;
                }
                catch (DbUpdateException)
                {
                    throw new UserException("Error occurred while adding a traveller.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while adding a traveller.");
                }
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }


        public async Task<Traveller?> Get(int key)
        {
            if (_userContext.Travellers != null)
            {
                var traveller = await _userContext.Travellers.FirstOrDefaultAsync(a => a.UserId == key);
                if (traveller != null)
                    return traveller;
                return null;
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }


        public async Task<ICollection<Traveller>?> GetAll()
        {
            if (_userContext.Travellers != null)
            {
                var travellers = await _userContext.Travellers.ToListAsync();
                return travellers;
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }
    }
}
