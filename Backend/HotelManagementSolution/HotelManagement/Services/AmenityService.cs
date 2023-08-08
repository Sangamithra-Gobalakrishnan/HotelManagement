using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.DTO;

namespace HotelManagement.Services
{
    public class AmenityService : IAmenityService<AmenityOrImageDTO, HotelFunctionDTO>
    {
        private readonly IAmenityRepo<Amenity, HotelFunctionDTO> _amenityRepo;

        public AmenityService(IAmenityRepo<Amenity, HotelFunctionDTO> amenityRepo)
        {
            _amenityRepo = amenityRepo;
        }

        public async Task<AmenityOrImageDTO?> Add(AmenityOrImageDTO amenityType)
        {
            var amenity = MapToAmenity(amenityType);
            var addedAmenity = await _amenityRepo.Add(amenity);
            return addedAmenity != null ? MapToDTO(addedAmenity) : null;
        }

        public async Task<AmenityOrImageDTO?> Delete(AmenityOrImageDTO amenityType)
        {
            var amenity = MapToAmenity(amenityType);
            var deletedAmenity = await _amenityRepo.Delete(amenity);
            return deletedAmenity != null ? MapToDTO(deletedAmenity) : null;
        }

        public async Task<ICollection<AmenityOrImageDTO>?> GetAllById(HotelFunctionDTO key)
        {
            var amenities = await _amenityRepo.GetAllById(key);
            return amenities?.Select(MapToDTO).ToList();
        }

        // Helper method to map AmenityOrImageDTO to Amenity
        private Amenity MapToAmenity(AmenityOrImageDTO dto)
        {
            return new Amenity
            {
                HotelId = dto.HotelId,
                AmenityType = dto.AmenityTypeOrImage
            };
        }

        // Helper method to map Amenity to AmenityOrImageDTO

        private AmenityOrImageDTO MapToDTO(Amenity amenity)
        {
            return new AmenityOrImageDTO
            {
                HotelId = amenity.HotelId,
                AmenityTypeOrImage = amenity.AmenityType
            };
        }
    }
}
