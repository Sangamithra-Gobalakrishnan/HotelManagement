using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public string? ImageUrl { get; set; }
    }
}
