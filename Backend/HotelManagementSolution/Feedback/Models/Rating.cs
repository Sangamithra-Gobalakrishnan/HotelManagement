using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public int HotelOrPackageId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? RatingValue { get; set; }

        [MaxLength(500, ErrorMessage = "Feedback message cannot exceed 500 characters.")]
        public string? FeedbackMessage { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? DateSubmitted { get; set; }
    }
}

