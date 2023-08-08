using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Exceptions;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.Context;

namespace UserAuthentication.Repository
{
    public class UserRepo : IUserRepo<User, int,string>
    {
        private readonly UserContext _userContext;

        public UserRepo(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<User?> Add(User user)
        {
            if (_userContext.Users != null)
            {
                try
                {
                    _userContext.Add(user);
                    await _userContext.SaveChangesAsync();
                    return user;
                }
                catch (DbUpdateException)
                {
                    throw new UserException("Error occurred while adding an user.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while adding an user.");
                }
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }

        public async Task<User?> Get(int id)
        {
            if (_userContext.Users != null)
            {
                var users = await _userContext.Users.FirstOrDefaultAsync(d => d.Id == id);
                if (users != null)
                    return users;
                return null;
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }

        public async Task<ICollection<User>?> GetAll()
        {
            if (_userContext.Users != null)
            {
                var users = await _userContext.Users.ToListAsync();
                return users;
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }

        public async Task<User?> GetIdByPhoneNo(string phoneNumber)
        {
            if (_userContext.Users != null)
            {
                var user = await _userContext.Users.FirstOrDefaultAsync(d => d.PhoneNumber == phoneNumber);
                if (user != null)
                    return user;
                return null;
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }

        public async Task<User?> Update(User user)
        {
            if (_userContext.Users != null)
            {
                var users = _userContext.Users.FirstOrDefault(d => d.Id == user.Id);
                if (users != null)
                {
                    users.Status = user.Status;
                    _userContext.Update(users);
                    await _userContext.SaveChangesAsync();
                    return users;
                }
                return null;
            }
            throw new DatabaseNullException("UserContext is not properly initialized.");
        }
    }
}
