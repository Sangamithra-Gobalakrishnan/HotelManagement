using HotelManagement.Exceptions;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.Context;
using HotelManagement.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository
{
    public class ImageRepo : IImageRepo<Image, HotelFunctionDTO>
    {
        private readonly HotelContext _hotelContext;

        public ImageRepo(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }

        public async Task<Image?> Add(Image image)
        {
            if (_hotelContext.Images != null)
            {
                try
                {
                    _hotelContext.Images.Add(image);
                    await _hotelContext.SaveChangesAsync();
                    return image;
                }
                catch (DbUpdateException)
                {
                    throw new HotelException("Error occurred while adding an image.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while adding an image.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<Image?> Delete(Image image)
        {
            if (_hotelContext.Images != null)
            {
                try
                {
                    var imageToDelete = await _hotelContext.Images
                        .Where(i => i.HotelId == image.HotelId && i.ImageUrl == image.ImageUrl)
                        .FirstOrDefaultAsync();

                    if (imageToDelete != null)
                    {
                        _hotelContext.Images.Remove(imageToDelete);
                        await _hotelContext.SaveChangesAsync();
                        return image;
                    }
                }
                catch (DbUpdateException)
                {
                    throw new HotelException("Error occurred while deleting an image.");
                }
                catch (SqlException)
                {
                    throw new SqlDatabaseException("SQL Server error occurred while deleting an image.");
                }
            }
            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }

        public async Task<ICollection<Image>?> GetAllById(HotelFunctionDTO key)
        {
            if (_hotelContext.Images != null)
            {
                var images = await _hotelContext.Images
                    .Where(i => i.HotelId == key.HotelId)
                    .ToListAsync();

                return images.Count > 0 ? images : null;
            }

            throw new DatabaseNullException("HotelContext is not properly initialized.");
        }
    }
}
