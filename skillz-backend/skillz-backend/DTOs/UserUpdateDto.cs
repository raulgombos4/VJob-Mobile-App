using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    // Represents data for updating user information.
    public class UserUpdateDto
    {
        // Gets or sets the username.
        public string Username { get; set; }

        // Gets or sets the password. (Required)
        [Required]
        public string Password { get; set; }
        
        // Gets or sets the phone number.
        public string PhoneNumber { get; set; }

        // Gets or sets the location.
        public string Location { get; set; }

        // Gets or sets the verification status.
        public bool Verified { get; set; }

        // Gets or sets the profile picture URL.
        public List<IFormFile> ProfilePicture { get; set; }
    }
}
