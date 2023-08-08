using Feedback.Interfaces;
using Feedback.Models.DTO;
using FeedbackAPI.ErrorMessage;
using FeedbackAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService<FeedbackDTO, HotelIdDTO> _feedbackService;
        private readonly ILogger<FeedbackController> _logger;
        private readonly Error _error;

        public FeedbackController(IFeedbackService<FeedbackDTO,HotelIdDTO> feedbackService,
                                  ILogger<FeedbackController> logger)
        {
            _feedbackService = feedbackService;
            _logger = logger;
            _error = new Error();
        }

        [HttpPost("ProvideFeedback")]
        [ProducesResponseType(typeof(FeedbackDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FeedbackDTO>> Add(FeedbackDTO feedback)
        {
            try
            {
                var feedbackAddResult = await _feedbackService.Add(feedback);
                if (feedbackAddResult != null)
                    return Ok("Feedback Successfully Added!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[0];
            return BadRequest(_error);
        }

        [HttpPost("FetchFeedbackByHotelId")]
        [ProducesResponseType(typeof(ICollection<FeedbackDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<FeedbackDTO>?>> FeedbacksByHotelId(HotelIdDTO hotelIdDTO)
        {
            try
            {
                var feedbacks = await _feedbackService.GetByHotelId(hotelIdDTO);
                if (feedbacks != null)
                    return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 2;
            _error.Message = new Message().messages[1];
            return NotFound(_error);
        }

        [HttpPost("FetchFeedbackByMonth")]
        [ProducesResponseType(typeof(ICollection<FeedbackDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<FeedbackDTO>?>> FeedbacksByMonth(HotelIdDTO hotelIdDTO)
        {
            try
            {
                var feedbacks = await _feedbackService.GetFeedbackByMonth(hotelIdDTO);
                if (feedbacks != null)
                    return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 2;
            _error.Message = new Message().messages[2];
            return NotFound(_error);
        }
    }
}
