using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    // Represents a review provided for a job.
    public class ReviewJob
    {
        // Gets or sets the unique identifier for the job review.
        [Key]
        public int IdReviewJ { get; set; }

        // Gets or sets the unique identifier of the reviewed job.
        public int IdJob { get; set; }

        // Gets or sets the rating given in the review.
        public int Rating { get; set; }

        // Gets or sets the reference to the reviewed job.
        public Job Job { get; set; }
    }
}
