using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skillz_backend.Repositories;
using skillz_backend.models;
using skillz_backend.Repositories.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace skillz_backend.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IWebHostEnvironment _environment;

        // Constructor to initialize repositories
        public JobService(IJobRepository jobRepository, IUserRepository userRepository, IBookingRepository bookingRepository, IWebHostEnvironment environment)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _environment = environment;
        }

        // Retrieves a job by its ID asynchronously
        public async Task<Job> GetJobByIdAsync(int jobId)
        {
            // Validation for a positive JobId
            if (jobId <= 0)
            {
                throw new ArgumentException("JobId should be a positive integer.");
            }

            return await _jobRepository.GetJobByIdAsync(jobId);
        }

        // Retrieves all jobs asynchronously
        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _jobRepository.GetAllJobsAsync();
        }

        // Retrieves jobs by title asynchronously
        public async Task<List<Job>> GetJobsByTitleAsync(string jobTitle)
        {
            // Validation for non-null and non-empty job title
            if (string.IsNullOrEmpty(jobTitle))
            {
                throw new ArgumentException("JobTitle cannot be null or empty.");
            }

            return await _jobRepository.GetJobsByTitleAsync(jobTitle);
        }

        // Retrieves jobs by user ID asynchronously
        public async Task<List<Job>> GetJobsByUserAsync(int userId)
        {
            // Validation for a positive UserID
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _jobRepository.GetJobsByUserAsync(userId);
        }

        // Retrieves jobs by experience asynchronously
        public async Task<List<Job>> GetJobsByExperienceAsync(int experiencedYears)
        {
            // Validation for non-negative experienced years
            if (experiencedYears < 0)
            {
                throw new ArgumentException("ExperiencedYears should be a non-negative integer.");
            }

            return await _jobRepository.GetJobsByExperienceAsync(experiencedYears);
        }

        // Creates a new job asynchronously
        public async Task CreateJobAsync(Job job, List<String>? images)
        {
            // Validation for non-null job object and required properties
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job), "Job object cannot be null.");
            }

            if (string.IsNullOrEmpty(job.JobTitle))
            {
                throw new ArgumentException("JobTitle cannot be null or empty.");
            }

            if (job.ExperiencedYears < 0)
            {
                throw new ArgumentException("ExperiencedYears should be a non-negative integer.");
            }

            if (job.IdUser <= 0)
            {
                throw new ArgumentException("Id of User should be a non-negative integer.");
            }

            // Check if the job with the same JobId already exists
            var existingJobById = await _jobRepository.GetJobByIdAsync(job.IdJob);
            if (existingJobById != null)
            {
                throw new InvalidOperationException("A job with the same JobId already exists.");
            }

            // Check if the associated user exists
            var existingUser = await _userRepository.GetUserByIdAsync(job.IdUser);
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with Id {job.IdUser} does not exist.");
            }

            // Create the job
            await _jobRepository.CreateJobAsync(job);

            // Save images
            if (images != null)
            {
                foreach (var image in images)
                {
                    var jobImage = new JobImage
                    {
                        ImageUrl = image,
                        JobId = job.IdJob
                    };

                    await _jobRepository.CreateJobImageAsync(jobImage);
                }
            }
        }



        // Updates an existing job asynchronously
        public async Task UpdateJobAsync(Job job, List<String> savedImagePaths)
        {
            // Validation for a valid job object and JobId
            if (job == null || job.IdJob <= 0)
            {
                throw new ArgumentException("Invalid job object or JobId.");
            }

            if (string.IsNullOrEmpty(job.JobTitle))
            {
                throw new ArgumentException("JobTitle cannot be null or empty.");
            }

            if (job.ExperiencedYears < 0)
            {
                throw new ArgumentException("ExperiencedYears should be a non-negative integer.");
            }

            if (job.IdUser <= 0)
            {
                throw new ArgumentException("Id of User should be a non-negative integer.");
            }

            // Check if the associated user exists
            var existingUser = await _userRepository.GetUserByIdAsync(job.IdUser);
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with Id {job.IdUser} does not exist.");
            }

            if (savedImagePaths != null)
            {
                await _jobRepository.DeleteJobImageAsync(job.IdJob);
                foreach (var image in savedImagePaths)
                {
                    var jobImage = new JobImage
                    {
                        ImageUrl = image,
                        JobId = job.IdJob
                    };

                    await _jobRepository.CreateJobImageAsync(jobImage);
                }
            }

            // Update the job
            await _jobRepository.UpdateJobAsync(job);
        }

        // Deletes a job by its ID asynchronously
        public async Task DeleteJobAsync(int jobId)
        {
            // Validation for a positive JobId
            if (jobId <= 0)
            {
                throw new ArgumentException("JobId should be a positive integer.");
            }

            // Delete the job
            await _jobRepository.DeleteJobAsync(jobId);
        }

        // Filters jobs based on title, date, and location asynchronously
        public async Task<List<Job>> FilterJobsAsync(string jobTitle, DateTime date, string location)
        {
            var allJobs = await _jobRepository.GetAllJobsAsync();
            var allBookings = await _bookingRepository.GetAllBookingsAsync();

            var filteredJobs = allJobs.ToList();

            if (!string.IsNullOrEmpty(jobTitle))
            {
                // Filter jobs by title
                filteredJobs = filteredJobs
                    .Where(job => job.JobTitle?.Equals(jobTitle, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();
            }

            if (date != default)
            {
                // Filter jobs based on available dates
                filteredJobs = filteredJobs
                    .Where(job =>
                    {
                        var jobIdUser = job.IdUser;
                        var bookingsForJob = allBookings
                            .Where(booking =>
                                booking.DateTime.Date == date.Date &&
                                (booking.ProviderUserId == jobIdUser) &&
                                booking.Status == BookingStatus.Accepted);

                        return !bookingsForJob.Any();
                    })
                    .ToList();
            }

            if (!string.IsNullOrEmpty(location))
            {
                // Filter jobs based on location
                var lowerCaseLocation = location.ToLower();
                var userIds = filteredJobs.Select(job => job.IdUser).Distinct();

                var filteredJobsWithLocation = new List<Job>();

                foreach (var userId in userIds)
                {
                    var userLocation = await _userRepository.GetUserLocationByIdAsync(userId);

                    if (userLocation != null && userLocation.ToLower() == lowerCaseLocation)
                    {
                        filteredJobsWithLocation.AddRange(filteredJobs.Where(job => job.IdUser == userId));
                    }
                }

                filteredJobs = filteredJobsWithLocation.ToList();
            }
            return filteredJobs;
        }

        public async Task<List<JobImage>> GetAllImagesAsync()
        {
            return await _jobRepository.GetAllImagesAsync();
        }

        public async Task<List<JobImage>> GetImagesByJobIdAsync(int jobId)
        {
            return await _jobRepository.GetImagesByJobIdAsync(jobId);
        }
    }
}
