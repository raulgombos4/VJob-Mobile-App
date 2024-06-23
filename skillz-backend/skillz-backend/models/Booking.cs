using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    // Represents a booking for a service between a client and a provider.
    public class Booking
    {
        // Gets or sets the unique identifier for the booking.
        [Key]
        public int BookingId { get; set; }

        // Gets or sets the unique identifier for the client user making the reservation.
        public int ClientUserId { get; set; }

        public int JobId { get; set; }
        // Gets or sets the associated client user for this booking.
        public User ClientUser { get; set; }

        // Gets or sets the unique identifier for the provider user offering the service.
        public int ProviderUserId { get; set; }

        // Gets or sets the associated provider user for this booking.
        public User ProviderUser { get; set; }

        // Gets or sets the date and time of the booking.
        public DateTime DateTime { get; set; }

        // Gets or sets additional details or notes for the booking.
        public string Details { get; set; }
        // Gets or sets the status of the booking (Pending, Accepted, or Declined).
        public BookingStatus Status { get; set; }

        public Job Job { get; set; }
    }

    // Enum representing the possible statuses for a booking.
    public enum BookingStatus
    {
        Pending,
        Accepted,
        Canceled,
        Rejected,
        Completed
    }
}
