using Microsoft.EntityFrameworkCore;

namespace Reservation.Models.Context
{
    public class BookingContext:DbContext
    {
        public BookingContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Booking>? Bookings { get; set; }
    }
}
