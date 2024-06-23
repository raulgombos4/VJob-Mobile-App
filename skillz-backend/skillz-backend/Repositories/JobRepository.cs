using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using skillz_backend.data;
using skillz_backend.models;
using static System.Net.Mime.MediaTypeNames;

namespace skillz_backend.Repositories
{
    // Repository implementation for job-related operations.
    public class JobRepository : IJobRepository
    {
        private readonly SkillzDbContext _dbContext;

        // Constructor that initializes the repository with the provided DbContext.
        public JobRepository(SkillzDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // Retrieves a job by its unique identifier.
        public async Task<Job> GetJobByIdAsync(int jobId)
        {
            // Asynchronously retrieves a job by ID from the database using EF Core's FindAsync.
            return await _dbContext.Jobs.FindAsync(jobId);
        }

        // Retrieves all jobs from the database.
        public async Task<List<Job>> GetAllJobsAsync()
        {
            // Asynchronously retrieves all jobs from the database using EF Core's ToListAsync.
            return await _dbContext.Jobs.ToListAsync();
        }

        // Retrieves jobs by their title.
        public async Task<List<Job>> GetJobsByTitleAsync(string jobTitle)
        {
            // Asynchronously retrieves jobs by title from the database using EF Core's Where and ToListAsync.
            return await _dbContext.Jobs.Where(j => j.JobTitle == jobTitle).ToListAsync();
        }

        // Retrieves jobs by the ID of the associated user.
        public async Task<List<Job>> GetJobsByUserAsync(int userId)
        {
            // Asynchronously retrieves jobs by the ID of the associated user from the database using EF Core's Where and ToListAsync.
            return await _dbContext.Jobs.Where(j => j.IdUser == userId).ToListAsync();
        }

        // Retrieves jobs by the required experienced years.
        public async Task<List<Job>> GetJobsByExperienceAsync(int experiencedYears)
        {
            // Asynchronously retrieves jobs by required experienced years from the database using EF Core's Where and ToListAsync.
            return await _dbContext.Jobs.Where(j => j.ExperiencedYears == experiencedYears).ToListAsync();
        }

        // Creates a new job in the database.
        public async Task CreateJobAsync(Job job)
        {
            // Adds the job to the DbContext and asynchronously saves changes to the database.
            _dbContext.Jobs.Add(job);
            await _dbContext.SaveChangesAsync();
        }

        // Updates an existing job in the database.
        public async Task UpdateJobAsync(Job job)
        {
            // Marks the job entity as modified in the DbContext and asynchronously saves changes to the database.
            _dbContext.Entry(job).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Deletes a job based on its ID from the database.
        public async Task DeleteJobAsync(int jobId)
        {
            // Finds the job by ID using EF Core's FindAsync.
            var jobToDelete = await _dbContext.Jobs.FindAsync(jobId);

            // Removes the job from the DbContext and asynchronously saves changes to the database if the job is found.
            if (jobToDelete != null)
            {
                _dbContext.Jobs.Remove(jobToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateJobImageAsync(JobImage jobImage)
        {
            _dbContext.JobImages.Add(jobImage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteJobImageAsync(int jobId)
        {
            // Find images with the given jobId
            List<JobImage> imagesToDelete = await _dbContext.JobImages
                .Where(image => image.JobId == jobId)
                .ToListAsync();

            foreach (var image in imagesToDelete)
            {
                // Remove each image individually
                _dbContext.JobImages.Remove(image);
            }

            // Save changes after removing all images
            await _dbContext.SaveChangesAsync();
        }


        public async Task<List<JobImage>> GetAllImagesAsync()
        {
            return await _dbContext.JobImages.ToListAsync();
        }

        public async Task<List<JobImage>> GetImagesByJobIdAsync(int jobId)
        {
            return await _dbContext.JobImages
                .Where(j => j.JobId == jobId)
                .ToListAsync();
        }
    }
}
