using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skillz_backend.models;
using skillz_backend.Repositories;
using skillz_backend.Repositories.Interfaces;
using skillz_backend.Services.Interfaces;

namespace skillz_backend.Services
{
    // Service responsible for booking-related operations.
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;

        // Constructor that initializes the service with the provided repositories.
        public BookingService(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        // Asynchronously retrieves a booking by its unique identifier.
        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            if (bookingId <= 0)
            {
                throw new ArgumentException("BookingId should be a positive integer.");
            }

            return await _bookingRepository.GetBookingByIdAsync(bookingId);
        }

        // Asynchronously retrieves all bookings.
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }

        // Asynchronously retrieves bookings by client ID.
        public async Task<List<Booking>> GetBookingsByClientAsync(int clientId)
        {
            if (clientId <= 0)
            {
                throw new ArgumentException("ClientId should be a positive integer.");
            }

            return await _bookingRepository.GetBookingsByClientAsync(clientId);
        }

        // Asynchronously retrieves bookings by provider ID.
        public async Task<List<Booking>> GetBookingsByProviderAsync(int providerId)
        {
            if (providerId <= 0)
            {
                throw new ArgumentException("ProviderId should be a positive integer.");
            }

            return await _bookingRepository.GetBookingsByProviderAsync(providerId);
        }

        // Asynchronously retrieves bookings by status.
        public async Task<List<Booking>> GetBookingsByStatusAsync(string status)
        {
            // Validation and conversion of the status string to BookingStatus enum.
            if (!Enum.TryParse<BookingStatus>(status, true, out var bookingStatus))
            {
                throw new ArgumentException($"Invalid BookingStatus: {status}");
            }

            return await _bookingRepository.GetBookingsByStatusAsync(bookingStatus);
        }

        // Asynchronously creates a new booking.
        public async Task CreateBookingAsync(Booking booking)
        {
            // Additional validation for the booking.
            ValidateBooking(booking);

            // Checks if the client user exists.
            var existingClient = await _userRepository.GetUserByIdAsync(booking.ClientUserId);
            if (existingClient == null)
            {
                throw new InvalidOperationException($"User with Id {booking.ClientUserId} does not exist.");
            }

            // Checks if the provider user exists.
            var existingProvider = await _userRepository.GetUserByIdAsync(booking.ProviderUserId);
            if (existingProvider == null)
            {
                throw new InvalidOperationException($"User with Id {booking.ProviderUserId} does not exist.");
            }

            await _bookingRepository.CreateBookingAsync(booking);
        }

        // Asynchronously updates an existing booking.
        public async Task UpdateBookingAsync(Booking booking)
        {
            // Additional validation for the booking.
            ValidateBooking(booking);

            // Checks if the client user exists.
            var existingClient = await _userRepository.GetUserByIdAsync(booking.ClientUserId);
            if (existingClient == null)
            {
                throw new InvalidOperationException($"User with Id {booking.ClientUserId} does not exist.");
            }

            // Checks if the provider user exists.
            var existingProvider = await _userRepository.GetUserByIdAsync(booking.ProviderUserId);
            if (existingProvider == null)
            {
                throw new InvalidOperationException($"User with Id {booking.ProviderUserId} does not exist.");
            }

            await _bookingRepository.UpdateBookingAsync(booking);
        }

        // Asynchronously deletes a booking by its ID.
        public async Task DeleteBookingAsync(int bookingId)
        {
            if (bookingId <= 0)
            {
                throw new ArgumentException("BookingId should be a positive integer.");
            }

            await _bookingRepository.DeleteBookingAsync(bookingId);
        }

        // Validates the booking object.
        private static void ValidateBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking), "Booking object cannot be null.");
            }
        }
    }
}
