using HotelManagement.ErrorMessage;
using HotelManagement.Interfaces;
using HotelManagement.Models.DTO;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("HotelCORS")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService<AmenityOrImageDTO, HotelFunctionDTO> _imageService;
        private readonly ILogger<ImageController> _logger;
        private readonly Error _error;

        public ImageController(IImageService<AmenityOrImageDTO, HotelFunctionDTO> imageService,
                                 ILogger<ImageController> logger)
        {
            _imageService = imageService;
            _logger = logger;
            _error = new Error();
        }

        [HttpPost("AddImage")]
        [ProducesResponseType(typeof(AmenityOrImageDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AmenityOrImageDTO>?> Add(AmenityOrImageDTO image)
        {
            try
            {
                var imageAddResult = await _imageService.Add(image);
                if (imageAddResult != null)
                    return Ok("Image Successfully Added!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[7];
            return BadRequest(_error);
        }

        [HttpDelete("DeleteImage")]
        [ProducesResponseType(typeof(AmenityOrImageDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AmenityOrImageDTO>?> Delete(AmenityOrImageDTO image)
        {
            try
            {
                var imageDeleteResult = await _imageService.Delete(image);
                if (imageDeleteResult != null)
                    return Ok("Image Successfully Deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _error.Id = 1;
            _error.Message = new Message().messages[8];
            return BadRequest(_error);
        }

        [HttpPost("FetchImagesByHotelId")]
        [ProducesResponseType(typeof(ICollection<AmenityOrImageDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<AmenityOrImageDTO>>?> FetchAmenityByHotelId(HotelFunctionDTO hotelFunctionDTO)
        {
            try
            {
                var images = await _imageService.GetAllById(hotelFunctionDTO);
                if (images != null)
                    return Ok(images);
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
