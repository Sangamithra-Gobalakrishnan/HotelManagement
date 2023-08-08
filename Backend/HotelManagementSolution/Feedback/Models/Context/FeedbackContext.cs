using Feedback.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedbackAPI.Models.Context
{
    public class FeedbackContext:DbContext
    {
        public FeedbackContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<HotelFeedback>? Feedbacks { get; set; }
    }
}
