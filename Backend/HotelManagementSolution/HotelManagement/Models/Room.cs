using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        [Required(ErrorMessage = "Room Id is required")]
        public int? RoomId { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1")]
        public int? Capacity { get; set; }

        [Required(ErrorMessage = "Room type is required")]
        [MaxLength(100, ErrorMessage = "Room type cannot exceed 100 characters")]
        public string? RoomType { get; set; }

    }
}
