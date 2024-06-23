using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    // Represents a user in the system.
    public class User
    {
        // Gets or sets the unique identifier for the user.
        public int UserId { get; set; }

        // Gets or sets the username of the user.
        [Required]
        public string Username { get; set; }

        // Gets or sets the age of the user.
        public int Age { get; set; }

        // Gets or sets the email address of the user.
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Gets or sets the phone number of the user.
        [Required]
        public string PhoneNumber { get; set; }

        // Gets or sets the location of the user.
        [Required]
        public string Location { get; set; }

        // Gets or sets a value indicating whether the user is verified.
        public bool Verified { get; set; }

        // Gets or sets the hash of the user's password.
        [Required]
        public byte[] PasswordHash { get; set; }

        // Gets or sets the salt used for password hashing.
        [Required]
        public byte[] PasswordSalt { get; set; }

        // Gets or sets the profile picture URL of the user.
        public string ProfilePicture { get; set; }

        // Gets or sets the list of jobs associated with the user.
        public List<Job> Jobs { get; set; }

        // Gets or sets the list of user reviews.
        public List<ReviewUser> ReviewsUser { get; set; }

        // Gets or sets the list of certificates associated with the user.
        public List<CertificatUser> UserCertificates { get; set; }

        // Gets or sets the list of badges earned by the user.
        public List<UserBadge> UserBadges { get; set; }

        public List<Booking> Bookings { get; set; }

    }
}
