using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using skillz_backend.DTOs;
using skillz_backend.models;
using skillz_backend.Services.Interfaces;

namespace skillz_backend.Controllers
{
    [ApiController]
    [Route("booking/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        // Constructor to inject IBookingService dependency
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        // Retrieves a booking by ID
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            // Validation for a positive BookingId
            if (bookingId <= 0)
            {
                return BadRequest("Invalid BookingId. It should be a positive integer.");
            }

            var booking = await _bookingService.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            return Ok(booking);
        }

        // Retrieves all bookings
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();

            return Ok(bookings);
        }

        // Retrieves bookings by client ID
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetBookingsByClient(int clientId)
        {
            // Validation for a positive ClientId
            if (clientId <= 0)
            {
                return BadRequest("Invalid ClientId. It should be a positive integer.");
            }

            var bookings = await _bookingService.GetBookingsByClientAsync(clientId);

            //if (bookings.Count == 0)
            //{
            //    return NotFound($"No bookings found for client with ClientId '{clientId}'.");
            //}

            return Ok(bookings);
        }

        // Retrieves bookings by provider ID
        [HttpGet("provider/{providerId}")]
        public async Task<IActionResult> GetBookingsByProvider(int providerId)
        {
            // Validation for a positive ProviderId
            if (providerId <= 0)
            {
                return BadRequest("Invalid ProviderId. It should be a positive integer.");
            }

            var bookings = await _bookingService.GetBookingsByProviderAsync(providerId);

            //if (bookings.Count == 0)
            //{
            //    return NotFound($"No bookings found for provider with ProviderId '{providerId}'.");
            //}

            return Ok(bookings);
        }

        // Retrieves bookings by status
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetBookingsByStatus(string status)
        {
            // Validation for a non-null and non-empty status
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Status cannot be null or empty.");
            }

            var bookings = await _bookingService.GetBookingsByStatusAsync(status);

            //if (bookings.Count == 0)
            //{
            //    return NotFound($"No bookings found with the status '{status}'.");
            //}

            return Ok(bookings);
        }

        // Creates a new booking
        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking([FromBody] BookingDto bookingDto)
        {
            // Validate the ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Manual mapping from BookingDto to Booking
                var booking = new Booking
                {
                    JobId = bookingDto.JobId,
                    ClientUserId = bookingDto.ClientUserId,
                    ProviderUserId = bookingDto.ProviderUserId,
                    DateTime = bookingDto.DateTime,
                    Details = bookingDto.Details,
                    Status = Enum.Parse<BookingStatus>(bookingDto.Status, true) // Ignore case sensitivity
                };

                await _bookingService.CreateBookingAsync(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new BookingDto
            {
                ClientUserId = bookingDto.ClientUserId,
                ProviderUserId = bookingDto.ProviderUserId,
                DateTime = bookingDto.DateTime,
                Details = bookingDto.Details,
                Status = bookingDto.Status
            };
        }

        // Updates an existing booking
        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] BookingDto bookingDto)
        {
            // Validate the ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBooking = await _bookingService.GetBookingByIdAsync(bookingId);

            if (existingBooking == null)
            {
                return NotFound($"Booking with BookingId '{bookingId}' not found.");
            }

            try
            {
                
                // Manual mapping from BookingDto to Booking
                existingBooking.ClientUserId = bookingDto.ClientUserId;
                existingBooking.ProviderUserId = bookingDto.ProviderUserId;
                existingBooking.DateTime = bookingDto.DateTime;
                existingBooking.Details = bookingDto.Details;
                existingBooking.Status = Enum.Parse<BookingStatus>(bookingDto.Status, true); // Ignore case sensitivity

                await _bookingService.UpdateBookingAsync(existingBooking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(bookingDto);
        }

        // Deletes a booking by ID
        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            // Validation for a positive BookingId
            if (bookingId <= 0)
            {
                return BadRequest("Invalid BookingId. It should be a positive integer.");
            }

            var booking = await _bookingService.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return NotFound($"Booking with BookingId '{bookingId}' not found.");
            }

            await _bookingService.DeleteBookingAsync(bookingId);

            return NoContent();
        }
    }
}
