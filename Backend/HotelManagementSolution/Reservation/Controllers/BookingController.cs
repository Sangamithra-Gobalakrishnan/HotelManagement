using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reservation.ErrorMessage;
using Reservation.Interfaces;
using Reservation.Models;
using Reservation.Models.DTO;

namespace Reservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("HotelCORS")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService<BookingDTO, int> _bookingService;
        private readonly ILogger<BookingController> _logger;
        private readonly Error _error;

        public BookingController(IBookingService<BookingDTO, int> bookingService,
                                ILogger<BookingController> logger)
        {
            _bookingService = bookingService;  
            _logger = logger;
            _error = new Error();
        }

        [HttpPost("Booking")]
        [ProducesResponseType(typeof(BookingDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookingDTO>> Add(BookingDTO booking)
        {
            try
            {
                var addRoomResult = await _bookingService.Add(booking);
                if (addRoomResult != null)
                    return Ok("Room successfully booked!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[0];
            return BadRequest(_error);
        }

        [HttpDelete("Cancel")]
        [ProducesResponseType(typeof(BookingDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookingDTO>> Delete(BookingId bookingId)
        {
            try
            {
                var cancelRoomResult = await _bookingService.Delete(bookingId.UserId);
                if (cancelRoomResult != null)
                    return Ok("Booking cancelled successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[1];
            return BadRequest(_error);
        }


        [HttpPost("FetchBookingByUserId")]
        [ProducesResponseType(typeof(ICollection<BookingDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<BookingDTO>?>> FetchBookingByUserId(BookingId bookingId)
        {
            try
            {
                var bookings = await _bookingService.GetByUserId(bookingId.UserId);
                if (bookings != null)
                    return Ok(bookings);
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
