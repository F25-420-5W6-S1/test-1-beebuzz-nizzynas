using BeeBuzz.Data.Entities;

namespace BeeBuzz.Data.Interfaces
{
    public interface IUserRepository : IBeeBuzzGenericRepository<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersForOrganizationAsync(int organizationId);
        Task<ApplicationUser?> GetByIdAsync(int userId);
    }
}