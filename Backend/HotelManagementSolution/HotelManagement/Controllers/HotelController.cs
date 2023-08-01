using HotelManagement.ErrorMessage;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService<HotelDTO, int> _hotelService;
        private readonly ILogger<HotelController> _logger;
        private readonly Error _error;

        public HotelController(IHotelService<HotelDTO,int> hotelService,
                                ILogger<HotelController> logger)
        {
            _hotelService = hotelService;
            _logger = logger;
            _error = new Error();
        }

        [HttpPost("AddHotelInformation")]
        [ProducesResponseType(typeof(HotelDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<HotelDTO> Add(HotelDTO hotel)
        {
            try
            {
                var hotelAddResult = _hotelService.Add(hotel);
                if (hotelAddResult != null)
                    return Ok("Hotel Information Successfully Added!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[0];
            return BadRequest(_error);
        }


        [HttpPut("UpdateHotelInformation")]
        [ProducesResponseType(typeof(HotelDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<HotelDTO> Update(HotelDTO hotel)
        {
            try
            {
                var hotelUpdateResult = _hotelService.Update(hotel);
                if (hotelUpdateResult != null)
                    return Ok("Hotel Information Successfully Updated!");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[1];
            return BadRequest(_error);
        }

        [HttpDelete("DeleteHotelInformation")]
        [ProducesResponseType(typeof(HotelDeleteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<HotelDeleteDTO> Delete(HotelDeleteDTO hotelDeleteDTO)
        {
            try
            {
                var hotelDeleteResult = _hotelService.Delete(hotelDeleteDTO.HotelId);
                if (hotelDeleteResult != null)
                    return Ok("Hotel Information Successfully Deleted!");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[2];
            return BadRequest(_error);
        }

        [HttpGet("FetchAllHotels")]
        [ProducesResponseType(typeof(ICollection<HotelDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ICollection<HotelDTO>> FetchAllHotels()
        {
            try
            {
                var hotels = _hotelService.GetAll();
                if (hotels != null)
                    return Ok(hotels);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 2;
            _error.Message = new Message().messages[3];
            return NotFound(_error);
        }

        [HttpGet("FetchHotelById")]
        [ProducesResponseType(typeof(ICollection<HotelDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ICollection<HotelDTO>> FetchHotelById(int hotelId)
        {
            try
            {
                var hotel = _hotelService.GetById(hotelId);
                if (hotel != null)
                    return Ok(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 2;
            _error.Message = new Message().messages[4];
            return NotFound(_error);
        }
    }
}
