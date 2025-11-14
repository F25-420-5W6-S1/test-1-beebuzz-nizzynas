using System.ComponentModel.DataAnnotations;

namespace BeeBuzz.Data.Entities
{
    public class Organization
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string OrganizationId { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
        public virtual ICollection<Beehive> Beehives { get; set; } = new HashSet<Beehive>();
    }
}