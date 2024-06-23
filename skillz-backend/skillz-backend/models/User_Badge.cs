using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    // Represents the association between a user and a badge.
    public class UserBadge
    {
        // Gets or sets the unique identifier for the user-badge association.
        [Key]
        public int UserBadgeId { get; set; }

        // Gets or sets the unique identifier of the user.
        public int IdUser { get; set; }

        // Gets or sets the unique identifier of the badge.
        public int IdBadge { get; set; }

        // Gets or sets the reference to the associated user.
        public User User { get; set; }

        // Gets or sets the reference to the associated badge.
        public Badge Badge { get; set; }
    }
}
