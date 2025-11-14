using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeeBuzz.Data.Repositories
{
    public class OrganizationRepository : BeeBuzzGenericGenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(ApplicationDbContext db, ILogger<BeeBuzzGenericGenericRepository<Organization>> logger)
                 : base(db, logger)
        {
        }

        public async Task<Organization?> GetByOrganizationIdAsync(string organizationId)
        {
            try
            {
                _logger.LogInformation($"GetByOrganizationIdAsync was called with organizationId: {organizationId}");
                return await _dbSet.FirstOrDefaultAsync(o => o.OrganizationId == organizationId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get organization by organizationId: {ex}");
                return null;
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersForOrganizationAsync(int organizationId)
        {
            try
            {
                _logger.LogInformation($"GetAllUsersForOrganizationAsync was called with organizationId: {organizationId}");
                return await _context.Users
            .Where(u => u.OrganizationId == organizationId)
               .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get users for organization: {ex}");
                return new List<ApplicationUser>();
            }
        }

        public async Task<IEnumerable<Beehive>> GetAllBeehivesForOrganizationAsync(int organizationId)
        {
            try
            {
                _logger.LogInformation($"GetAllBeehivesForOrganizationAsync was called with organizationId: {organizationId}");
                return await _context.Beehives
                     .Where(b => b.OrganizationId == organizationId)
                    .Include(b => b.User)
                 .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get beehives for organization: {ex}");
                return new List<Beehive>();
            }
        }
    }
}