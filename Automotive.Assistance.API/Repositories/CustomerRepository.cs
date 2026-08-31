// Automotive.Assistance.API.Repositories/CustomerRepository.cs
using Automotive.Assistance.API.Data;
using Automotive.Assistance.API.Models.Entities;
using Automotive.Assistance.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Automotive.Assistance.API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Customer> _dbSet;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Customer>();
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.User)
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Customer>> GetAllActiveAsync()
        {
            return await _dbSet
                .Include(c => c.User)
                .Where(c => c.IsActive)
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer?> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(c => c.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Customer?> GetByCustomerCodeAsync(string customerCode)
        {
            return await _dbSet
                .Include(c => c.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerCode == customerCode);
        }

        public async Task<Customer> CreateAsync(Customer entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Customer> UpdateAsync(Customer entity)
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
            return await _dbSet.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<bool> ExistsByUserIdAsync(int userId)
        {
            return await _dbSet.AnyAsync(c => c.UserId == userId);
        }

        public async Task<bool> ExistsByCustomerCodeAsync(string customerCode)
        {
            return await _dbSet.AnyAsync(c => c.CustomerCode == customerCode);
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync(c => c.IsActive);
        }

        public async Task<List<Customer>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _dbSet
                .Include(c => c.User)
                .Where(c => c.IsActive)
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Customer>> GetCustomersByDateOfBirthRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(c => c.User)
                .Where(c => c.DateOfBirth >= startDate && c.DateOfBirth <= endDate)
                .AsNoTracking()
                .OrderBy(c => c.DateOfBirth)
                .ToListAsync();
        }

        public async Task<List<Customer>> SearchAsync(string searchTerm)
        {
            searchTerm = searchTerm.ToLower().Trim();

            return await _dbSet
                .Include(c => c.User)
                .Where(c =>
                    (c.User.FirstName + " " + c.User.LastName).ToLower().Contains(searchTerm) ||
                    c.User.Email.ToLower().Contains(searchTerm) ||
                    c.User.MobileNumber.Contains(searchTerm) ||
                    c.CustomerCode.ToLower().Contains(searchTerm))
                .AsNoTracking()
                .OrderBy(c => c.CustomerId)
                .ToListAsync();
        }
    }
}