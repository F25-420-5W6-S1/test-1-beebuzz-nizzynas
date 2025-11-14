using System.ComponentModel.DataAnnotations;

namespace BeeBuzz.Data.Entities
{
    public enum BeehiveStatus
    {
        Active,
        Inactive
    }

    public enum DeactivationReason
    {
        None,
        Dead,
        Sold
    }

    public class Beehive
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Location { get; set; } = string.Empty; // Address/Location

        public BeehiveStatus Status { get; set; } = BeehiveStatus.Active;

        public DeactivationReason? ReasonForDeactivation { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeactivatedAt { get; set; }

        // Foreign keys
        public int OrganizationId { get; set; }
        public int UserId { get; set; } // The user managing this beehive

        // Navigation properties
        public virtual Organization Organization { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}