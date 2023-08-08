using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace UserAuthentication.Models.Context
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Traveller>? Travellers { get; set; }
        public DbSet<TravelAgent>? TravelAgents { get; set; }
    }
}
