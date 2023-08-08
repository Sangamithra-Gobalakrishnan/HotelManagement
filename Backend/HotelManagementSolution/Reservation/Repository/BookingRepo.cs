using Microsoft.EntityFrameworkCore;
using Reservation.Interfaces;
using Reservation.Models;
using Reservation.Models.Context;

namespace Reservation.Repository
{
    public class BookingRepo : IBookingRepo<Booking, int>
    {
        private readonly BookingContext _bookingContext;

        public BookingRepo(BookingContext bookingContext)
        {
            _bookingContext = bookingContext; 
        }
        public async Task<Booking?> Add(Booking booking)
        {
            if (_bookingContext.Bookings != null)
            {
                _bookingContext.Bookings.Add(booking);
                await _bookingContext.SaveChangesAsync();
                return booking;
            }
            return null;
        }

        public async Task<Booking?> Delete(int key)
        {
            if (_bookingContext.Bookings != null)
            {
                var existingBooking = await _bookingContext.Bookings.FirstOrDefaultAsync(b => b.Id == key);
                if (existingBooking != null)
                {
                    _bookingContext.Bookings.Remove(existingBooking);
                    await _bookingContext.SaveChangesAsync();
                }
                return existingBooking;
            }
            throw new Exception();
        }

        public async Task<ICollection<Booking>?> GetAll()
        {
            if(_bookingContext.Bookings != null)
            {
                return await _bookingContext.Bookings.ToListAsync();
            }
            throw new Exception();
        }

        public async Task<Booking?> GetById(int key)
        {
            if(_bookingContext.Bookings != null)
            {
                 return await _bookingContext.Bookings.FirstOrDefaultAsync(b => b.Id == key);
            }
            throw new Exception();
        }

        public async Task<ICollection<Booking>?> GetByUserId(int key)
        {
            if(_bookingContext.Bookings != null)
            {
                return await _bookingContext.Bookings.Where(b => b.UserId == key).ToListAsync();
            }
            throw new Exception();
        }
    }
}
