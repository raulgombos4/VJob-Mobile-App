using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace skillz_backend.models
{
    // Represents a job offered by a user.
    public class Job
    {
        // Gets or sets the unique identifier for the job.
        [Key]
        public int IdJob { get; set; }
        
        // Gets or sets the title of the job.
        public string JobTitle { get; set; }

        // Gets or sets the description of the job.
        public string Description { get; set; }

        // Gets or sets the number of years of experience required for the job.
        public int ExperiencedYears { get; set; }

        // Gets or sets the list of images associated with the job.
        public List<JobImage> Images { get; set; }

        // Gets or sets the unique identifier of the user offering the job.
        public int IdUser { get; set; }

        // Gets or sets the reference to the user offering the job.
        [JsonIgnore] // Ignored for JSON serialization to prevent circular reference.
        public User User { get; set; }

        // Gets or sets the list of reviews provided for the job.
        public List<ReviewJob> ReviewsJob { get; set; }

        [JsonIgnore]
        public List<Booking> Bookings { get; set; }
    }
}
