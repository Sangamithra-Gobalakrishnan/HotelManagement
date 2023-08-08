using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserAuthentication.Models
{
    public class TravelAgent
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Id")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }

        [StringLength(50, ErrorMessage = "City should not exceed 50 characters.")]
        public string? City { get; set; }

        [StringLength(50, ErrorMessage = "State should not exceed 50 characters.")]
        public string? State { get; set; }

        [StringLength(50, ErrorMessage = "Country should not exceed 50 characters.")]
        public string? Country { get; set; }
    }
}
