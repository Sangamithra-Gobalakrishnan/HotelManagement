using System.ComponentModel.DataAnnotations;

namespace UserAuthentication.Models.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string? Password { get; set; }
        public int UserId { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public string? Status { get; set; }
    }
}
