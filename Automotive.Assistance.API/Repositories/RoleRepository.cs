using Automotive.Assistance.API.Data;
using Automotive.Assistance.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Automotive.Assistance.API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Role> CreateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRoleAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetRoleByIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Role>> GetRolesListAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public Task<Role> UpdateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
