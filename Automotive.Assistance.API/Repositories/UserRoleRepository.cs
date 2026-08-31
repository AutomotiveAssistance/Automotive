// Automotive.Assistance.API.Repositories/UserRoleRepository.cs
using Automotive.Assistance.API.Data;
using Automotive.Assistance.API.Models.Entities;
using Automotive.Assistance.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Automotive.Assistance.API.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<UserRole> _dbSet;

        public UserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<UserRole>();
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(ur => ur.UserRoleId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetAllActiveAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(ur => ur.IsActive)
                .OrderBy(ur => ur.UserRoleId)
                .ToListAsync();
        }

        public async Task<UserRole?> GetByIdAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(ur => ur.UserRoleId == id);
        }

        public async Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(ur => ur.UserId == userId && ur.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetByRoleIdAsync(int roleId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(ur => ur.RoleId == roleId && ur.IsActive)
                .ToListAsync();
        }

        public async Task<UserRole?> GetByUserAndRoleIdAsync(int userId, int roleId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public async Task<bool> IsUserInRoleAsync(int userId, int roleId)
        {
            return await _dbSet
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId && ur.IsActive);
        }

        public async Task<UserRole> CreateAsync(UserRole entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserRole> UpdateAsync(UserRole entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "SYSTEM";

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(ur => ur.UserRoleId == id);
        }

        public async Task<bool> ExistsByUserAndRoleAsync(int userId, int roleId)
        {
            return await _dbSet
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public async Task<int> CountByRoleAsync(int roleId)
        {
            return await _dbSet
                .CountAsync(ur => ur.RoleId == roleId && ur.IsActive);
        }

        public async Task<int> CountByUserAsync(int userId)
        {
            return await _dbSet
                .CountAsync(ur => ur.UserId == userId && ur.IsActive);
        }

        public async Task<IEnumerable<UserRole>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(ur => ur.UserRoleId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetByUserIdsAsync(List<int> userIds)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(ur => userIds.Contains(ur.UserId) && ur.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetByRoleIdsAsync(List<int> roleIds)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(ur => roleIds.Contains(ur.RoleId) && ur.IsActive)
                .ToListAsync();
        }
    }
}