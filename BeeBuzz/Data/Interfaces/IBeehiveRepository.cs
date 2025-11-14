using BeeBuzz.Data.Entities;

namespace BeeBuzz.Data.Interfaces
{
    public interface IBeehiveRepository : IBeeBuzzGenericRepository<Beehive>
    {
        Task<IEnumerable<Beehive>> GetAllBeehivesForOrganizationAsync(int organizationId);
        Task<IEnumerable<Beehive>> GetBeehivesForUserAsync(int userId);
        Task<Beehive?> GetByIdAsync(int beehiveId);
    }
}