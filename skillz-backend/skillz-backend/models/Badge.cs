using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    // Represents a badge that can be earned by users.
    public class Badge
    {
        // Gets or sets the unique identifier for the badge.
        [Key]
        public int BadgeId { get; set; }

        // Gets or sets the type of the badge.
        public string BadgeType { get; set; }

        // Gets or sets the icon associated with the badge.
        public string Icon { get; set; }

        // Gets or sets the list of user badges associated with this badge.
        public List<UserBadge> UserBadges { get; set; }
    }
}
