using System.ComponentModel.DataAnnotations;

namespace skillz_backend.DTOs
{
    // Represents data for user registration.
    public class RegisterDto
    {
        public int Id { get; set; }
        // Gets or sets the username.
        [Required]
        public string Username { get; set; }

        // Gets or sets the password.
        [Required]
        public string Password { get; set; }

        // Gets or sets the email.
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Gets or sets the phone number.
        [Required]
        public string PhoneNumber { get; set; }

        // Gets or sets the location.
        [Required]
        public string Location { get; set; }
    }
}
