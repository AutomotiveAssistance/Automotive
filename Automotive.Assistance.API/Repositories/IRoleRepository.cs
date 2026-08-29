using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetRolesListAsync();
    }
}
