using HotelManagement.Exceptions;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository
{
    public class HotelRepo : IHotelRepo<Hotel, int>
    {
        private readonly HotelContext _hotelContext;

        public HotelRepo(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }

        public async Task<Hotel?> Add(Hotel hotel)
        {
            if (_hotelContext.Hotels != null)
            {
                try
                {
                    _hotelContext.Hotels.Add(hotel);
                    await _hotelContext.SaveChangesAsync();
                    return hotel;
                }
                catch (DbUpdateException)
                { 
                    throw new HotelException("Error occurred while adding a hotel.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while adding a hotel.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<Hotel?> Delete(int key)
        {
            if (_hotelContext.Hotels != null)
            {
                try
                {
                    var hotel = await _hotelContext.Hotels.FindAsync(key);
                    if (hotel != null)
                    {
                        _hotelContext.Hotels.Remove(hotel);
                        await _hotelContext.SaveChangesAsync();
                        return hotel;
                    }
                    return null;
                }
                catch (DbUpdateException)
                {
                    throw new HotelException("Error occurred while deleting a hotel.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while deleting a hotel.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<ICollection<Hotel>?> GetAll()
        {
            if (_hotelContext.Hotels != null)
            {
                try
                {
                    return await _hotelContext.Hotels.ToListAsync();
                }
                catch (DbUpdateException)
                {
                    throw new HotelException("Error occurred while fetching hotels.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while fetching hotels.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<Hotel?> GetById(int key)
        {
            if (_hotelContext.Hotels != null)
            {
                try
                {
                    return await _hotelContext.Hotels.FindAsync(key);
                }
                catch (DbUpdateException)
                {
                    throw new HotelException($"Error occurred while getting the hotel with ID {key}.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException($"SQL Server error occurred while getting the hotel with ID {key}.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<Hotel?> Update(Hotel hotel)
        {
            if (_hotelContext.Hotels != null)
            {
                try
                {
                    var existingHotel = await _hotelContext.Hotels.FindAsync(hotel.Id);
                    if (existingHotel != null)
                    {
                        _hotelContext.Entry(existingHotel).CurrentValues.SetValues(hotel);
                        await _hotelContext.SaveChangesAsync();
                        return hotel;
                    }
                    return null;
                }
                catch (DbUpdateException)
                {
                    throw new HotelException("Error occurred while updating the hotel.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while updating the hotel.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }
    }
}
