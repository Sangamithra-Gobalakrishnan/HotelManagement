using Feedback.Interfaces;
using Feedback.Models.DTO;
using Feedback.Models;
using Microsoft.EntityFrameworkCore;
using FeedbackAPI.Models.Context;
using Microsoft.Data.SqlClient;
using FeedbackAPI.Exceptions;

namespace Feedback.Repository
{
    public class FeedbackRepo : IFeedbackRepo<HotelFeedback, HotelIdDTO>
    {
        private readonly FeedbackContext _feedbackContext;

        public FeedbackRepo(FeedbackContext feedbackContext)
        {
           _feedbackContext = feedbackContext;
        }

        public async Task<HotelFeedback?> Add(HotelFeedback feedback)
        {
            if(_feedbackContext.Feedbacks != null)
            {
                try
                {
                    _feedbackContext.Feedbacks.Add(feedback);
                    await _feedbackContext.SaveChangesAsync();
                    return feedback;
                }
                catch (DbUpdateException)
                {
                    throw new FeedbackException("Error occurred while adding a hotel.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while adding a hotel.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<ICollection<HotelFeedback>?> GetByHotelId(HotelIdDTO hotelId)
        {
            if(_feedbackContext.Feedbacks != null)
            {
                var feedbacks = await _feedbackContext.Feedbacks
               .Where(f => f.HotelId == hotelId.HotelId)
               .ToListAsync();

                return feedbacks.Count > 0 ? feedbacks : null;
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<ICollection<HotelFeedback>?> GetFeedbackByMonth(HotelIdDTO month)
        {
            if (_feedbackContext.Feedbacks != null)
            {
                var startDate = DateTime.Now.AddMonths(-month.Month).Date;
                var endDate = DateTime.Now.Date;

                var feedbacks = await _feedbackContext.Feedbacks
                    .Where(f => f.HotelId == month.HotelId &&
                                f.DateSubmitted.HasValue &&
                                f.DateSubmitted.Value >= startDate &&
                                f.DateSubmitted.Value <= endDate)
                    .ToListAsync();

                return feedbacks.Count > 0 ? feedbacks : null;
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }
    }
}
