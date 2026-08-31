// Automotive.Assistance.API.Repositories/CustomerAddressRepository.cs
using Automotive.Assistance.API.Data;
using Automotive.Assistance.API.Models.Entities;
using Automotive.Assistance.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Automotive.Assistance.API.Repositories
{
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<CustomerAddress> _dbSet;

        public CustomerAddressRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<CustomerAddress>();
        }

        public async Task<List<CustomerAddress>> GetAllAsync()
        {
            return await _dbSet
                .Include(x => x.Customer)
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<CustomerAddress>> GetAllActiveAsync()
        {
            return await _dbSet
                .Include(x => x.Customer)
                .Where(x => x.IsActive)
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<CustomerAddress?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(x => x.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerAddressId == id);
        }

        public async Task<List<CustomerAddress>> GetByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .Include(x => x.Customer)
                .Where(x => x.CustomerId == customerId)
                .AsNoTracking()
                .OrderByDescending(x => x.IsDefault)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<CustomerAddress>> GetActiveAddressesByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .Include(x => x.Customer)
                .Where(x => x.CustomerId == customerId && x.IsActive)
                .AsNoTracking()
                .OrderByDescending(x => x.IsDefault)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<CustomerAddress?> GetDefaultAddressByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .Include(x => x.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.IsDefault && x.IsActive);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(x => x.CustomerAddressId == id);
        }

        public async Task<bool> HasDefaultAddressAsync(int customerId)
        {
            return await _dbSet.AnyAsync(x => x.CustomerId == customerId && x.IsDefault && x.IsActive);
        }

        public async Task<CustomerAddress> CreateAsync(CustomerAddress entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CustomerAddress> UpdateAsync(CustomerAddress entity)
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

        public async Task<bool> RemoveDefaultFlagForCustomerAsync(int customerId, int excludeAddressId = 0)
        {
            var addresses = await _dbSet
                .Where(x => x.CustomerId == customerId && x.IsDefault && x.CustomerAddressId != excludeAddressId)
                .ToListAsync();

            if (!addresses.Any())
                return true;

            foreach (var address in addresses)
            {
                address.IsDefault = false;
                address.UpdatedAt = DateTime.UtcNow;
                address.UpdatedBy = "SYSTEM";
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, bool isActive)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            entity.IsActive = isActive;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "SYSTEM";

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetAddressCountByCustomerIdAsync(int customerId)
        {
            return await _dbSet.CountAsync(x => x.CustomerId == customerId && x.IsActive);
        }
    }
}