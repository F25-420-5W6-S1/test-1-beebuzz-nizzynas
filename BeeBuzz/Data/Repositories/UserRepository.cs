using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeeBuzz.Data.Repositories
{
    public class UserRepository : BeeBuzzGenericGenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db, ILogger<BeeBuzzGenericGenericRepository<ApplicationUser>> logger)
             : base(db, logger)
        {
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersForOrganizationAsync(int organizationId)
        {
            try
            {
                _logger.LogInformation($"GetAllUsersForOrganizationAsync was called with organizationId: {organizationId}");
                return await _dbSet
                       .Where(u => u.OrganizationId == organizationId)
                          .Include(u => u.Organization)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get users for organization: {ex}");
                return new List<ApplicationUser>();
            }
        }

        public async Task<ApplicationUser?> GetByIdAsync(int userId)
        {
            try
            {
                _logger.LogInformation($"GetByIdAsync was called with userId: {userId}");
                return await _dbSet
                  .Include(u => u.Organization)
                  .Include(u => u.Beehives)
                    .FirstOrDefaultAsync(u => u.Id == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user by id: {ex}");
                return null;
            }
        }
    }
}