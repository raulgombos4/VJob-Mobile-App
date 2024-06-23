using System.ComponentModel.DataAnnotations;

namespace skillz_backend.DTOs
{
    // Represents data for a booking.
    public class BookingDto
    {
        // Gets or sets the client user ID.
        [Required]
        public int ClientUserId { get; set; }

        // Gets or sets the provider user ID.
        [Required]
        public int ProviderUserId { get; set; }

        // Gets or sets the date and time of the booking.
        [Required]
        public DateTime DateTime { get; set; }

        public int JobId { get; set; }

        // Gets or sets additional details for the booking.
        public string Details { get; set; }

        // Gets or sets the status of the booking.
        [Required]
        public string Status { get; set; }
    }
}
