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
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService<AmenityOrImageDTO, HotelFunctionDTO> _amenityService;
        private readonly ILogger<AmenityController> _logger;
        private readonly Error _error;

        public AmenityController(IAmenityService<AmenityOrImageDTO,HotelFunctionDTO> amenityService,
                                 ILogger<AmenityController> logger)
        {
            _amenityService = amenityService;
            _logger = logger;
            _error = new Error();
        }

        [HttpPost("AddAmenityInformation")]
        [ProducesResponseType(typeof(AmenityOrImageDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AmenityOrImageDTO>?> Add(AmenityOrImageDTO amenity)
        {
            try
            {
                var amenityAddResult = await _amenityService.Add(amenity);
                if (amenityAddResult != null)
                    return Ok("Amenity Information Successfully Added!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[7];
            return BadRequest(_error);
        }

        [HttpDelete("DeleteAmenityInformation")]
        [ProducesResponseType(typeof(AmenityOrImageDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AmenityOrImageDTO>?> Delete(AmenityOrImageDTO amenity)
        {
            try
            {
                var amenityDeleteResult = await _amenityService.Delete(amenity);
                if (amenityDeleteResult != null)
                    return Ok("Amenity Information Successfully Deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[8];
            return BadRequest(_error);
        }

        [HttpPost("FetchAmenitiesByHotelId")]
        [ProducesResponseType(typeof(ICollection<AmenityOrImageDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<AmenityOrImageDTO>>?> FetchAmenityByHotelId(HotelFunctionDTO hotelFunctionDTO)
        {
            try
            {
                var amenities = await _amenityService.GetAllById(hotelFunctionDTO);
                if (amenities != null)
                    return Ok(amenities);
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
