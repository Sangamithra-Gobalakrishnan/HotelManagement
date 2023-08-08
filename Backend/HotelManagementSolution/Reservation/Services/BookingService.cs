using Reservation.Interfaces;
using Reservation.Models;
using Reservation.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reservation.Services
{
    public class BookingService : IBookingService<BookingDTO, int>
    {
        private readonly IBookingRepo<Booking, int> _bookingRepo;

        public BookingService(IBookingRepo<Booking, int> bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public async Task<BookingDTO?> Add(BookingDTO bookingDTO)
        {
            Booking booking = MapDTOToModel(bookingDTO);
            if (booking.StartDate >= booking.EndDate)
                return null;

            if (HasOverlappingBookings(booking))
                return null;

            Booking? addedBooking = await _bookingRepo.Add(booking);

            return addedBooking != null ? MapModelToDTO(addedBooking) : null;
        }

        public async Task<BookingDTO?> Delete(int key)
        {
            Booking? bookingToDelete = await _bookingRepo.GetById(key);
            if (bookingToDelete == null)
                return null; // Booking not found

            // Check if cancellation is allowed based on end date
            if (bookingToDelete.EndDate <= DateTime.Now)
                return null; // Cancellation not allowed

            Booking? deletedBooking = await _bookingRepo.Delete(key);
            return deletedBooking != null ? MapModelToDTO(deletedBooking) : null;
        }

        public async Task<ICollection<BookingDTO>?> GetByUserId(int key)
        {
            ICollection<Booking?> bookings = await _bookingRepo.GetByUserId(key);
            if (bookings != null && bookings.Count > 0)
                return bookings?.Select(MapModelToDTO).ToList();
            return null;
        }

        private Booking MapDTOToModel(BookingDTO bookingDTO)
        {
            return new Booking
            {
                Id = bookingDTO.Id,
                UserId = bookingDTO.UserId,
                HotelId = bookingDTO.HotelId,
                RoomId = bookingDTO.RoomId,
                StartDate = bookingDTO.StartDate,
                EndDate = bookingDTO.EndDate,
                ChildCount = bookingDTO.ChildCount,
                AdultCount = bookingDTO.AdultCount
            };
        }

        private BookingDTO MapModelToDTO(Booking booking)
        {
            return new BookingDTO
            {
                Id = booking.Id,
                UserId = booking.UserId,
                HotelId = booking.HotelId,
                RoomId = booking.RoomId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                ChildCount = booking.ChildCount,
                AdultCount = booking.AdultCount
            };
        }

        private bool HasOverlappingBookings(Booking booking)
        {
            // Logic to check for overlapping bookings based on room and date ranges
            foreach (var existingBooking in _bookingRepo.GetAll().Result)
            {
                if (existingBooking.RoomId == booking.RoomId &&
                    existingBooking.StartDate <= booking.EndDate &&
                    existingBooking.EndDate >= booking.StartDate)
                {
                    return true; // Found overlapping booking
                }
            }
            return false; // No overlapping bookings found
        }
    }
}
