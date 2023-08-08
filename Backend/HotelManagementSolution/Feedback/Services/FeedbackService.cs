using Feedback.Interfaces;
using Feedback.Models.DTO;
using Feedback.Models;

namespace Feedback.Services
{
    public class FeedbackService : IFeedbackService<FeedbackDTO, HotelIdDTO>
    {
        private readonly IFeedbackRepo<HotelFeedback, HotelIdDTO> _feedbackRepo;

        public FeedbackService(IFeedbackRepo<HotelFeedback, HotelIdDTO> feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
        }

        public async Task<FeedbackDTO?> Add(FeedbackDTO feedbackDTO)
        {
            HotelFeedback feedback = MapDTOToModel(feedbackDTO);
            HotelFeedback? addedFeedback = await _feedbackRepo.Add(feedback);

            if (addedFeedback != null)
            {
                return MapModelToDTO(addedFeedback);
            }
            return null;
        }

        public async Task<ICollection<FeedbackDTO>?> GetByHotelId(HotelIdDTO hotelIdDTO)
        {
            ICollection<HotelFeedback>? feedbacks = await _feedbackRepo.GetByHotelId(hotelIdDTO);
            return feedbacks?.Select(MapModelToDTO).ToList();
        }

        public async Task<ICollection<FeedbackDTO>?> GetFeedbackByMonth(HotelIdDTO month)
        {
            var feedbacks = await _feedbackRepo.GetFeedbackByMonth(month);
            return feedbacks?.Select(MapModelToDTO).ToList();
        }

        // Helper method to map FeedbackDTO to HotelFeedback
        private HotelFeedback MapDTOToModel(FeedbackDTO feedbackDTO)
        {
            return new HotelFeedback
            {
                Id = feedbackDTO.Id,
                HotelId = feedbackDTO.HotelId,
                RatingValue = feedbackDTO.RatingValue,
                FeedbackMessage = feedbackDTO.FeedbackMessage,
                DateSubmitted = feedbackDTO.DateSubmitted
            };
        }

        // Helper method to map HotelFeedback to FeedbackDTO
        private FeedbackDTO MapModelToDTO(HotelFeedback feedback)
        {
            return new FeedbackDTO
            {
                Id = feedback.Id,
                HotelId = feedback.HotelId,
                RatingValue = feedback.RatingValue,
                FeedbackMessage = feedback.FeedbackMessage,
                DateSubmitted = feedback.DateSubmitted
            };
        }
    }
}
