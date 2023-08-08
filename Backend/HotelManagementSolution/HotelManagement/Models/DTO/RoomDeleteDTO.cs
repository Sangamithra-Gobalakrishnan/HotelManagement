using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.DTO
{
    public class RoomDeleteDTO
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        public int? RoomId { get; set; }
    }
}
