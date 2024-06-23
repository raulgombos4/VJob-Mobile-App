using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using skillz_backend.models;

namespace skillz_backend.Services.Interfaces
{
    // Interface defining operations related to the Booking entity in the service.
    public interface IBookingService
    {
        // Retrieves a booking by its unique identifier.
        Task<Booking> GetBookingByIdAsync(int bookingId);

        // Retrieves a list of all bookings.
        Task<List<Booking>> GetAllBookingsAsync();

        // Retrieves bookings associated with a client by their unique identifier.
        Task<List<Booking>> GetBookingsByClientAsync(int clientId);

        // Retrieves bookings associated with a provider by their unique identifier.
        Task<List<Booking>> GetBookingsByProviderAsync(int providerId);

        // Retrieves bookings by their status.
        Task<List<Booking>> GetBookingsByStatusAsync(string status);

        // Creates a new booking in the service.
        Task CreateBookingAsync(Booking booking);

        // Updates an existing booking in the service.
        Task UpdateBookingAsync(Booking booking);

        // Deletes a booking by its unique identifier from the service.
        Task DeleteBookingAsync(int bookingId);
    }
}
