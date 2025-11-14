using BeeBuzz.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeeBuzz.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                          serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Create roles if they don't exist
            string[] roleNames = { "Admin", "Default" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<int> { Name = roleName });
                }
            }

            // Create default organization if it doesn't exist
            var defaultOrg = await context.Organizations
     .FirstOrDefaultAsync(o => o.OrganizationId == "0000-0000-0000-0000");

            if (defaultOrg == null)
            {
                defaultOrg = new Organization
                {
                    OrganizationId = "0000-0000-0000-0000",
                    Name = "Default Organization",
                    Description = "Initial organization for the BeeBuzz system",
                    CreatedAt = DateTime.UtcNow
                };

                context.Organizations.Add(defaultOrg);
                await context.SaveChangesAsync();
            }

            // Create admin user if it doesn't exist
            const string adminEmail = "admin@beebuzz.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    OrganizationId = defaultOrg.Id,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}