using BeeBuzz.Data.Entities;

namespace BeeBuzz.Data.Interfaces
{
    public interface IOrganizationRepository : IBeeBuzzGenericRepository<Organization>
    {
        Task<Organization?> GetByOrganizationIdAsync(string organizationId);
        Task<IEnumerable<ApplicationUser>> GetAllUsersForOrganizationAsync(int organizationId);
        Task<IEnumerable<Beehive>> GetAllBeehivesForOrganizationAsync(int organizationId);
    }
}