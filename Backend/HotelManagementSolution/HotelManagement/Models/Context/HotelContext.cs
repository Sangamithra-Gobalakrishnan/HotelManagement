using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models.Context
{
    public class HotelContext:DbContext
    {
        public HotelContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hotel>? Hotels { get; set; }
        public DbSet<Amenity>?Amenities { get; set; }
        public DbSet<Image>? Images { get; set;}
        public DbSet<Room>? Rooms { get; set;}
    }
}
