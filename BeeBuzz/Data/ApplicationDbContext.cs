using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BeeBuzz.Data.Entities;

namespace BeeBuzz.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Beehive> Beehives { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Organization
            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasIndex(e => e.OrganizationId)
                    .IsUnique();

                entity.HasMany(e => e.Users)
                    .WithOne(e => e.Organization)
                    .HasForeignKey(e => e.OrganizationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Beehives)
                    .WithOne(e => e.Organization)
                    .HasForeignKey(e => e.OrganizationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Beehive
            modelBuilder.Entity<Beehive>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Beehives)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure ApplicationUser
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
            });
        }
    }
}
