using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using skillz_backend.models;

namespace skillz_backend.Services
{
    // Interface defining operations related to the Job entity in the service.
    public interface IJobService
    {
        // Retrieves a job by its unique identifier.
        Task<Job> GetJobByIdAsync(int jobId);

        // Retrieves a list of all jobs.
        Task<List<Job>> GetAllJobsAsync();

        // Retrieves jobs by their title.
        Task<List<Job>> GetJobsByTitleAsync(string jobTitle);

        // Retrieves jobs associated with a user by their unique identifier.
        Task<List<Job>> GetJobsByUserAsync(int userId);

        // Retrieves jobs by the required experience.
        Task<List<Job>> GetJobsByExperienceAsync(int experiencedYears);

        // Creates a new job in the service.
        Task CreateJobAsync(Job job, List<String> images);

        // Updates an existing job in the service.
        Task UpdateJobAsync(Job job, List<String> savedImagePaths);

        // Deletes a job by its unique identifier from the service.
        Task DeleteJobAsync(int jobId);

        // Filters jobs based on specified criteria.
        Task<List<Job>> FilterJobsAsync(string jobTitle, DateTime date, string location);

        Task<List<JobImage>> GetAllImagesAsync();
        Task<List<JobImage>> GetImagesByJobIdAsync(int jobId);
    }
}
