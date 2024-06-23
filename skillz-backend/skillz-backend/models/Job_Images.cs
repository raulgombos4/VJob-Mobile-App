using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    // Represents an image associated with a job.
    public class JobImage
    {
        // Gets or sets the unique identifier for the image.
        [Key]
        public int IdImage { get; set; }

        // Gets or sets the URL of the image.
        public string ImageUrl { get; set; }

        // Gets or sets the unique identifier of the job associated with the image.
        public int JobId { get; set; }

        // Gets or sets the reference to the job associated with the image.
        public Job Job { get; set; }
    }
}
