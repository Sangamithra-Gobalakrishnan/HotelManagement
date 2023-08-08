using HotelManagement.Exceptions;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.Context;
using HotelManagement.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository
{
    public class AmenityRepo : IAmenityRepo<Amenity, HotelFunctionDTO>
    {
        private readonly HotelContext _hotelContext;

        public AmenityRepo(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }

        public async Task<Amenity?> Add(Amenity amenityType)
        {
            if (_hotelContext.Amenities != null)
            {
                try
                {
                    _hotelContext.Amenities.Add(amenityType);
                    await _hotelContext.SaveChangesAsync();
                    return amenityType;
                }
                catch (DbUpdateException)
                {
                    throw new HotelException("Error occurred while adding an amenity.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while adding an amenity.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<Amenity?> Delete(Amenity amenityType)
        {
            if(_hotelContext.Amenities != null)
            {
                try
                {
                    var amenity = await _hotelContext.Amenities
                    .Where(a => a.HotelId == amenityType.HotelId && a.AmenityType == amenityType.AmenityType).FirstOrDefaultAsync();

                    if (amenity != null)
                    {
                        _hotelContext.Amenities.Remove(amenity);
                        await _hotelContext.SaveChangesAsync();
                        return amenityType;
                    }
                }
                catch (DbUpdateException)
                {
                    throw new HotelException("Error occurred while deleting an amenity.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while deleting an amenity.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<ICollection<Amenity>?> GetAllById(HotelFunctionDTO key)
        {
            if (_hotelContext.Amenities != null)
            {
                var amenities = await _hotelContext.Amenities
                    .Where(a => a.HotelId == key.HotelId)
                    .ToListAsync();

                return amenities.Count > 0 ? amenities : null;
            }

            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }
    }
}
