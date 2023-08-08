using System.ComponentModel.DataAnnotations;

namespace Feedback.Models.DTO
{
    public class HotelIdDTO
    {
        [Required]
        public int HotelId { get; set; }

        public int Month { get; set; }
    }
}
