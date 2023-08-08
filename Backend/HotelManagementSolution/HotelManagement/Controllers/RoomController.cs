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
    public class RoomController : ControllerBase
    {
        private readonly IRoomService<RoomDTO,RoomDeleteDTO,HotelFunctionDTO> _roomService;
        private readonly ILogger<RoomController> _logger;
        private readonly Error _error;

        public RoomController(IRoomService<RoomDTO, RoomDeleteDTO, HotelFunctionDTO> roomService,
                              ILogger<RoomController> logger)
        {
            _roomService = roomService;
            _logger = logger;
            _error = new Error();
        }

        [HttpPost("AddRoomInformation")]
        [ProducesResponseType(typeof(RoomDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RoomDTO>?> Add(RoomDTO room)
        {
            try
            {
                var roomAddResult = await _roomService.Add(room);
                if (roomAddResult != null)
                    return Ok("Room Information Successfully Added!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[5];
            return BadRequest(_error);
        }

        [HttpPut("UpdateRoomInformation")]
        [ProducesResponseType(typeof(RoomDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RoomDTO>> Update(RoomDTO room)
        {
            try
            {
                var roomUpdateResult = await _roomService.Update(room);
                if (roomUpdateResult != null)
                    return Ok("Room Information Successfully Updated!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[1];
            return BadRequest(_error);
        }

        [HttpDelete("DeleteRoomInformation")]
        [ProducesResponseType(typeof(RoomDeleteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RoomDeleteDTO>?> Delete(RoomDeleteDTO roomDeleteDTO)
        {
            try
            {
                var roomDeleteResult = await _roomService.Delete(roomDeleteDTO);
                if (roomDeleteResult != null)
                    return Ok("Room Information Successfully Deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[6];
            return BadRequest(_error);
        }

        [HttpPost("FetchRoomsByHotelId")]
        [ProducesResponseType(typeof(ICollection<RoomDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<RoomDTO>>?> FetchAmenityByHotelId(HotelFunctionDTO hotelFunctionDTO)
        {
            try
            {
                var rooms = await _roomService.GetAllById(hotelFunctionDTO);
                if (rooms != null)
                    return Ok(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 2;
            _error.Message = new Message().messages[9];
            return NotFound(_error);
        }
    }
}
