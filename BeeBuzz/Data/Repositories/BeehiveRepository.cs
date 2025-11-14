using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeeBuzz.Data.Repositories
{
    public class BeehiveRepository : BeeBuzzGenericGenericRepository<Beehive>, IBeehiveRepository
    {
        public BeehiveRepository(ApplicationDbContext db, ILogger<BeeBuzzGenericGenericRepository<Beehive>> logger)
      : base(db, logger)
        {
        }

        public async Task<IEnumerable<Beehive>> GetAllBeehivesForOrganizationAsync(int organizationId)
        {
            try
            {
                _logger.LogInformation($"GetAllBeehivesForOrganizationAsync was called with organizationId: {organizationId}");
                return await _dbSet
                 .Where(b => b.OrganizationId == organizationId)
            .Include(b => b.User)
             .Include(b => b.Organization)
               .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get beehives for organization: {ex}");
                return new List<Beehive>();
            }
        }

        public async Task<IEnumerable<Beehive>> GetBeehivesForUserAsync(int userId)
        {
            try
            {
                _logger.LogInformation($"GetBeehivesForUserAsync was called with userId: {userId}");
                return await _dbSet
                  .Where(b => b.UserId == userId)
                    .Include(b => b.User)
                     .Include(b => b.Organization)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get beehives for user: {ex}");
                return new List<Beehive>();
            }
        }

        public async Task<Beehive?> GetByIdAsync(int beehiveId)
        {
            try
            {
                _logger.LogInformation($"GetByIdAsync was called with beehiveId: {beehiveId}");
                return await _dbSet
        .Include(b => b.User)
         .Include(b => b.Organization)
             .FirstOrDefaultAsync(b => b.Id == beehiveId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get beehive by id: {ex}");
                return null;
            }
        }
    }
}