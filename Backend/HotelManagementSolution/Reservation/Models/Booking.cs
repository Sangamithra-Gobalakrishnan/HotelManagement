using System.ComponentModel.DataAnnotations;

namespace Reservation.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int UserId { get;set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public int ChildCount { get; set; }
        
        [Required]
        public int AdultCount { get; set; }
    }
}
