using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class Amenity
    { 
            [Key]
            public int Id { get; set; }

            [ForeignKey("Hotel")]
            public int HotelId { get; set; }
            public Hotel? Hotel { get; set; }
            public string? AmenityType { get; set; }
        }
}
