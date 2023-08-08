using HotelManagement.ErrorMessage;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("HotelCORS")]
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
        public async Task<ActionResult<HotelDTO>> Add(HotelDTO hotel)
        {
            try
            {
                var hotelAddResult = await _hotelService.Add(hotel);
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
        public async Task<ActionResult<HotelDTO>> Update(HotelDTO hotel)
        {
            try
            {
                var hotelUpdateResult = await _hotelService.Update(hotel);
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
        [ProducesResponseType(typeof(HotelFunctionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HotelFunctionDTO>> Delete(HotelFunctionDTO hotelDeleteDTO)
        {
            try
            {
                var hotelDeleteResult = await _hotelService.Delete(hotelDeleteDTO.HotelId);
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
        public async Task<ActionResult<ICollection<HotelDTO>?>> FetchAllHotels()
        {
            try
            {
                var hotels = await _hotelService.GetAll();
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

        [HttpPost("FetchHotelById")]
        [ProducesResponseType(typeof(ICollection<HotelDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<HotelDTO>?>> FetchHotelById(HotelFunctionDTO hotelFunctionDTO)
        {
            try
            {
                var hotel = await _hotelService.GetById(hotelFunctionDTO.HotelId);
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

        [HttpPost("FetchHotelByAgentId")]
        [ProducesResponseType(typeof(ICollection<HotelDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<HotelDTO>?>> FetchHotelByAgentId(HotelFunctionDTO hotelFunctionDTO)
        {
            try
            {
                var hotel = await _hotelService.GetByAgentId(hotelFunctionDTO.HotelId);
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
