using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using skillz_backend.data;
using skillz_backend.models;
using skillz_backend.Repositories.Interfaces;

namespace skillz_backend.Repositories
{
    // Repository implementation for booking-related operations.
    public class BookingRepository : IBookingRepository
    {
        private readonly SkillzDbContext _dbContext;

        // Constructor that initializes the repository with the provided DbContext.
        public BookingRepository(SkillzDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // Retrieves a booking by its unique identifier.
        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            // Asynchronously retrieves a booking by ID from the database using EF Core's FindAsync.
            return await _dbContext.Bookings.FindAsync(bookingId);
        }

        // Retrieves all bookings from the database.
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            // Asynchronously retrieves all bookings from the database using EF Core's ToListAsync.
            return await _dbContext.Bookings.ToListAsync();
        }

        // Retrieves bookings by the ID of the associated client.
        public async Task<List<Booking>> GetBookingsByClientAsync(int clientId)
        {
            // Asynchronously retrieves bookings by the ID of the associated client from the database using EF Core's Where and ToListAsync.
            return await _dbContext.Bookings.Where(b => b.ClientUserId == clientId).ToListAsync();
        }

        // Retrieves bookings by the ID of the associated provider.
        public async Task<List<Booking>> GetBookingsByProviderAsync(int providerId)
        {
            // Asynchronously retrieves bookings by the ID of the associated provider from the database using EF Core's Where and ToListAsync.
            return await _dbContext.Bookings.Where(b => b.ProviderUserId == providerId).ToListAsync();
        }

        // Retrieves bookings by status.
        public async Task<List<Booking>> GetBookingsByStatusAsync(BookingStatus status)
        {
            // Asynchronously retrieves bookings by status from the database using EF Core's Where and ToListAsync.
            return await _dbContext.Bookings.Where(b => b.Status == status).ToListAsync();
        }

        // Creates a new booking in the database.
        public async Task CreateBookingAsync(Booking booking)
        {
            // Adds the booking to the DbContext and asynchronously saves changes to the database.
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
        }

        // Updates an existing booking in the database.
        public async Task UpdateBookingAsync(Booking booking)
        {
            // Marks the booking entity as modified in the DbContext and asynchronously saves changes to the database.
            _dbContext.Entry(booking).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Deletes a booking based on its ID from the database.
        public async Task DeleteBookingAsync(int bookingId)
        {
            // Finds the booking by ID using EF Core's FindAsync.
            var bookingToDelete = await _dbContext.Bookings.FindAsync(bookingId);

            // Removes the booking from the DbContext and asynchronously saves changes to the database if the booking is found.
            if (bookingToDelete != null)
            {
                _dbContext.Bookings.Remove(bookingToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
