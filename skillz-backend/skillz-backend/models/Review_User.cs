using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    // Represents a review provided by a user.
    public class ReviewUser
    {
        // Gets or sets the unique identifier for the user review.
        [Key]
        public int IdReviewU { get; set; }

        // Gets or sets the unique identifier of the reviewed user.
        public int IdUser { get; set; }

        // Gets or sets the rating given in the review.
        public int Rating { get; set; }

        // Gets or sets the reference to the reviewed user.
        public User User { get; set; }
    }
}
