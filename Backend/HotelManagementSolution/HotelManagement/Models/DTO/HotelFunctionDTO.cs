using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.DTO
{
    public class HotelFunctionDTO
    {
        [Required]
        public int HotelId { get; set; }
    }
}
