using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.DTO;


namespace HotelManagement.Services
{
    public class ImageService : IImageService<AmenityOrImageDTO, HotelFunctionDTO>
    {
        private readonly IImageRepo<Image, HotelFunctionDTO> _imageRepo;

        public ImageService(IImageRepo<Image, HotelFunctionDTO> imageRepo)
        {
            _imageRepo = imageRepo;
        }

        public async Task<AmenityOrImageDTO?> Add(AmenityOrImageDTO imageType)
        {
            var image = MapToImage(imageType);
            var addedImage = await _imageRepo.Add(image);
            return addedImage != null ? MapToDTO(addedImage) : null;
        }

        public async Task<AmenityOrImageDTO?> Delete(AmenityOrImageDTO imageType)
        {
            var image = MapToImage(imageType);
            var deletedImage = await _imageRepo.Delete(image);
            return deletedImage != null ? MapToDTO(deletedImage) : null;
        }

        public async Task<ICollection<AmenityOrImageDTO>?> GetAllById(HotelFunctionDTO key)
        {
            var images = await _imageRepo.GetAllById(key);
            return images?.Select(MapToDTO).ToList();
        }

        // Helper method to map AmenityOrImageDTO to Image

        private Image MapToImage(AmenityOrImageDTO dto)
        {
            return new Image
            {
                HotelId = dto.HotelId,
                ImageUrl = dto.AmenityTypeOrImage 
            };
        }

        // Helper method to map Image to AmenityOrImageDTO
        private AmenityOrImageDTO MapToDTO(Image image)
        {
            return new AmenityOrImageDTO
            {
                HotelId = image.HotelId,
                AmenityTypeOrImage = image.ImageUrl
            };
        }
    }
}
