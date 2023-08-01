using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.DTO
{
    public class HotelDeleteDTO
    {
        [Required]
        public int HotelId { get; set; }
    }
}
