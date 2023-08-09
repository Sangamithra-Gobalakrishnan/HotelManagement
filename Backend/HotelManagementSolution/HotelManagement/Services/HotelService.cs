using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.DTO;

namespace HotelManagement.Services
{
    public class HotelService : IHotelService<HotelDTO, int>
    {
        private readonly IHotelRepo<Hotel, int> _hotelRepo;
        private readonly IAmenityRepo<Amenity, HotelFunctionDTO> _amenityRepo;
        private readonly IImageRepo<Image, HotelFunctionDTO> _imageRepo;

        public HotelService(IHotelRepo<Hotel, int> hotelRepo,
                            IAmenityRepo<Amenity,HotelFunctionDTO> amenityRepo,
                            IImageRepo<Image, HotelFunctionDTO> imageRepo)
        {
            _hotelRepo = hotelRepo;
            _amenityRepo = amenityRepo;
            _imageRepo = imageRepo;
        }

        public async Task<HotelDTO?> Add(HotelDTO hotelDTO)
        {
            Hotel hotel = MapDTOToModel(hotelDTO);
            Hotel? addedHotel = await _hotelRepo.Add(hotel);

            if (addedHotel != null)
            {
                //bool amenitiesCreated = hotel.AmenityType == null || hotel.AmenityType.All(amenity => _amenityRepo.Add(amenity) != null);
                //bool imagesCreated = hotel.Images == null || hotel.Images.All(image => _imageRepo.Add(image) != null);
                //if (amenitiesCreated && imagesCreated)
                    return MapModelToDTO(addedHotel);
            }
            return null;
        }
        public async Task<HotelDTO?> Delete(int key)
        {
            Hotel? deletedHotel = await _hotelRepo.Delete(key);
            if(deletedHotel != null)
              return MapModelToDTO(deletedHotel);
            return null;
        }

        public async Task<ICollection<HotelDTO>?> GetAll()
        {
            ICollection<Hotel>? hotels = await _hotelRepo.GetAll();
            return hotels?.Select(MapModelToDTO).ToList();
        }

        public async Task<ICollection<HotelDTO>?> GetByAgentId(int key)
        {
            ICollection<Hotel?> hotels = await _hotelRepo.GetByAgentId(key);
            if(hotels != null && hotels.Count > 0)
               return hotels?.Select(MapModelToDTO).ToList();
            return null;

        }
        public async Task<HotelDTO?> GetById(int key)
        {
            Hotel? hotel = await _hotelRepo.GetById(key);
            if (hotel != null)
                return MapModelToDTO(hotel);
            return null;
        }

        public async Task<HotelDTO?> Update(HotelDTO hotelDTO)
        {
            Hotel hotel = MapDTOToModel(hotelDTO);
            Hotel? updatedHotel = await _hotelRepo.Update(hotel);
            if(updatedHotel != null)
              return MapModelToDTO(updatedHotel);
            return null;
        }

        // Helper method to map HotelDTO to Hotel
        private Hotel MapDTOToModel(HotelDTO hotelDTO)
        {
            return new Hotel
            {
                Id = hotelDTO.Id,
                AgentId = hotelDTO.AgentId,
                Name = hotelDTO.Name,
                Description = hotelDTO.Description,
                Email = hotelDTO.Email,
                Address = hotelDTO.Address,
                ContactNumber = hotelDTO.ContactNumber,
                City = hotelDTO.City,
                Country = hotelDTO.Country,
                NumberOfRooms = hotelDTO.NumberOfRooms,
                MinimumPriceRange = hotelDTO.MinimumPriceRange,
                MaximumPriceRange = hotelDTO.MaximumPriceRange,
                Images = hotelDTO.Images,
                AmenityType = hotelDTO.AmenityType,
            };
        }

        // Helper method to map Hotel to HotelDTO
        private HotelDTO MapModelToDTO(Hotel hotel)
        {
            return new HotelDTO
            {
                Id = hotel.Id,
                AgentId = hotel.AgentId,
                Name = hotel.Name,
                Description = hotel.Description,
                Email = hotel.Email,
                Address = hotel.Address,
                ContactNumber = hotel.ContactNumber,
                City = hotel.City,
                Country = hotel.Country,
                NumberOfRooms = hotel.NumberOfRooms,
                MinimumPriceRange = hotel.MinimumPriceRange,
                MaximumPriceRange = hotel.MaximumPriceRange,
                Images = hotel.Images,
                AmenityType = hotel.AmenityType,
            };
        }
    }
}
