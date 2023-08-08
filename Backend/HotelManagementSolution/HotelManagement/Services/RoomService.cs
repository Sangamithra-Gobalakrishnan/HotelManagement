using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.DTO;

namespace HotelManagement.Services
{
    public class RoomService : IRoomService<RoomDTO, RoomDeleteDTO, HotelFunctionDTO>
    {
        private readonly IRoomRepo<Room, RoomDeleteDTO, HotelFunctionDTO> _roomRepo;

        public RoomService(IRoomRepo<Room, RoomDeleteDTO, HotelFunctionDTO> roomRepo)
        {
            _roomRepo = roomRepo;
        }
        public async Task<RoomDTO?> Add(RoomDTO roomDTO)
        {
            Room room = MapDTOToModel(roomDTO);
            Room? addedRoom = await _roomRepo.Add(room);

            if (addedRoom != null)
                return MapModelToDTO(addedRoom);
            return null;
        }

        public async Task<RoomDeleteDTO?> Delete(RoomDeleteDTO roomDeleteDTO)
        {
            RoomDeleteDTO? deletedRoom = await _roomRepo.Delete(roomDeleteDTO);
            if (deletedRoom != null)
                return deletedRoom;
            return null;
        }

        public async Task<ICollection<RoomDTO>?> GetAllById(HotelFunctionDTO key)
        {
            var rooms = await _roomRepo.GetAllById(key);
            return rooms?.Select(MapModelToDTO).ToList();
        }

        public async Task<RoomDTO?> Update(RoomDTO roomDTO)
        {
            Room room = MapDTOToModel(roomDTO);
            Room? updatedRoom = await _roomRepo.Update(room);

            if (updatedRoom != null)
                return MapModelToDTO(updatedRoom);
            return null;
        }

        // Helper method to map RoomDTO to Room
        private Room MapDTOToModel(RoomDTO roomDTO)
        {
            return new Room
            {
                Id = roomDTO.Id,
                HotelId = roomDTO.HotelId,
                RoomId = roomDTO.RoomId,
                Price = roomDTO.Price,
                Capacity = roomDTO.Capacity,
                RoomType = roomDTO.RoomType,
            };
        }

        // Helper method to map Room to RoomDTO
        private RoomDTO MapModelToDTO(Room room)
        {
            return new RoomDTO
            {
                Id = room.Id,
                HotelId = room.HotelId,
                RoomId = room.RoomId,
                Price = room.Price,
                Capacity = room.Capacity,
                RoomType = room.RoomType,
            };
        }
    }
}
