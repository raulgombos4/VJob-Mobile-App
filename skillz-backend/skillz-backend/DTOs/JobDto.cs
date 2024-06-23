using skillz_backend.models;

namespace skillz_backend.DTOs
{
    // Represents data for a job.
    public class JobDto
    {
        // Gets or sets the job title.
        public string JobTitle { get; set; }

        // Gets or sets the job description.
        public string Description { get; set; }

        // Gets or sets the number of experienced years required for the job.
        public int ExperiencedYears { get; set; }

        // Gets or sets the user ID associated with the job.
        public int IdUser { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
