using HotelManagement.Exceptions;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.Context;
using HotelManagement.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository
{
    public class RoomRepo : IRoomRepo<Room, RoomDeleteDTO, HotelFunctionDTO>
    {
        private readonly HotelContext _hotelContext;

        public RoomRepo(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }

        public async Task<Room?> Add(Room room)
        {
            if (_hotelContext.Rooms != null)
            {
                try
                {
                    _hotelContext.Rooms.Add(room);
                    await _hotelContext.SaveChangesAsync();
                    return room;
                }
                catch (DbUpdateException)
                {
                    throw new HotelException("Error occurred while adding a room.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while adding a room.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<RoomDeleteDTO?> Delete(RoomDeleteDTO roomDeleteDTO)
        {
            if (_hotelContext.Rooms != null)
            {
                try
                {
                    Room? roomToDelete = await _hotelContext.Rooms
                        .FirstOrDefaultAsync(r => r.HotelId == roomDeleteDTO.HotelId && r.RoomId == roomDeleteDTO.RoomId);

                    if (roomToDelete != null)
                    {
                        _hotelContext.Rooms.Remove(roomToDelete);
                        await _hotelContext.SaveChangesAsync();
                        return roomDeleteDTO;
                    }

                    throw new RoomNotFoundException("Room not found.");
                }
                catch (DbUpdateException)
                {
                    throw new HotelException("Error occurred while deleting the room.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while deleting the room.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<ICollection<Room>?> GetAllById(HotelFunctionDTO key)
        {
            if (_hotelContext.Rooms != null)
            {
                var rooms = await _hotelContext.Rooms
                    .Where(r => r.HotelId == key.HotelId)
                    .ToListAsync();

                return rooms.Count > 0 ? rooms : null;
            }

            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<Room?> Update(Room room)
        {
            if (_hotelContext.Rooms != null)
            {
                try
                {
                    var existingRoom = await _hotelContext.Rooms
                        .FirstOrDefaultAsync(r => r.HotelId == room.HotelId && r.RoomId == room.RoomId);

                    if (existingRoom != null)
                    {
                        _hotelContext.Entry(existingRoom).CurrentValues.SetValues(room);

                        await _hotelContext.SaveChangesAsync();
                        return existingRoom;
                    }

                    throw new RoomNotFoundException("Room not found.");
                }
                catch (DbUpdateException)
                {
                    throw new HotelException("Error occurred while updating the room.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while updating the room.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }
    }
}
