using System;
using System.ComponentModel.DataAnnotations;

namespace UserAuthentication.Models
{
    public class User
    {
        public User()
        {
            CreatedAt = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name should not exceed 50 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Username should not exceed 20 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? EmailId { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }
        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordKey { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Status { get; set; }
    }
}
