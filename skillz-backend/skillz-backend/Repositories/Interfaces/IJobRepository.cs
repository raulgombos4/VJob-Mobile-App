using System.Collections.Generic;
using System.Threading.Tasks;
using skillz_backend.models;

namespace skillz_backend.Repositories
{
    // Interface defining operations related to the Job entity in the repository.
    public interface IJobRepository
    {
        // Retrieves a job by its unique identifier.
        Task<Job> GetJobByIdAsync(int jobId);

        // Retrieves a list of all jobs.
        Task<List<Job>> GetAllJobsAsync();

        // Retrieves jobs by their title.
        Task<List<Job>> GetJobsByTitleAsync(string jobTitle);

        // Retrieves jobs associated with a user by their unique identifier.
        Task<List<Job>> GetJobsByUserAsync(int userId);

        // Retrieves jobs by the required experience level.
        Task<List<Job>> GetJobsByExperienceAsync(int experiencedYears);

        Task<List<JobImage>> GetAllImagesAsync();
        Task<List<JobImage>> GetImagesByJobIdAsync(int jobId);

        // Creates a new job in the repository.
        Task CreateJobAsync(Job job);

        // Updates an existing job in the repository.
        Task UpdateJobAsync(Job job);

        // Deletes a job by its unique identifier from the repository.
        Task DeleteJobAsync(int jobId);

        Task CreateJobImageAsync(JobImage jobImage);

        Task DeleteJobImageAsync(int jobId);    
    }
}
