using Automotive.Assistance.API.Data;
using Automotive.Assistance.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Automotive.Assistance.API.Repositories
{
    public class ServiceProviderUserRepository : IServiceProviderUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceProviderUserRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceProviderUser>> GetServiceProviderUsersListAsync()
        {
            return await _context.ServiceProviderUsers.Include(x => x.User).Include(x => x.ServiceProvider).AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<List<ServiceProviderUser>>
            GetUsersByServiceProviderIdAsync(
                int serviceProviderId)
        {
            return await _context.ServiceProviderUsers
                .Include(x => x.User)
                .Include(x => x.ServiceProvider)
                .Where(x =>
                    x.ServiceProviderId == serviceProviderId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ServiceProviderUser>>
            GetServiceProvidersByUserIdAsync(
                int userId)
        {
            return await _context.ServiceProviderUsers
                .Include(x => x.User)
                .Include(x => x.ServiceProvider)
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ServiceProviderUser?>
            GetServiceProviderUserByIdAsync(
                int serviceProviderUserId)
        {
            return await _context.ServiceProviderUsers
                .Include(x => x.User)
                .Include(x => x.ServiceProvider)
                .FirstOrDefaultAsync(x =>
                    x.ServiceProviderUserId ==
                    serviceProviderUserId);
        }

        public async Task<ServiceProviderUser?>
            GetServiceProviderUserAsync(
                int serviceProviderId,
                int userId)
        {
            return await _context.ServiceProviderUsers
                .Include(x => x.User)
                .Include(x => x.ServiceProvider)
                .FirstOrDefaultAsync(x =>
                    x.ServiceProviderId == serviceProviderId &&
                    x.UserId == userId);
        }

        public async Task<ServiceProviderUser?>
            GetPrimaryContactAsync(
                int serviceProviderId)
        {
            return await _context.ServiceProviderUsers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x =>
                    x.ServiceProviderId == serviceProviderId &&
                    x.IsPrimaryContact &&
                    x.IsActive);
        }

        public async Task<ServiceProviderUser>
            CreateServiceProviderUserAsync(
                ServiceProviderUser serviceProviderUser)
        {
            await _context.ServiceProviderUsers
                .AddAsync(serviceProviderUser);

            await _context.SaveChangesAsync();

            return serviceProviderUser;
        }

        public async Task<ServiceProviderUser>
            UpdateServiceProviderUserAsync(
                ServiceProviderUser serviceProviderUser)
        {
            _context.ServiceProviderUsers
                .Update(serviceProviderUser);

            await _context.SaveChangesAsync();

            return serviceProviderUser;
        }

        public async Task<bool>
            DeleteServiceProviderUserAsync(
                int serviceProviderUserId)
        {
            var entity =
                await _context.ServiceProviderUsers
                    .FirstOrDefaultAsync(x =>
                        x.ServiceProviderUserId ==
                        serviceProviderUserId);

            if (entity == null)
                return false;

            _context.ServiceProviderUsers.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool>
            ServiceProviderUserExistsAsync(
                int serviceProviderId,
                int userId)
        {
            return await _context.ServiceProviderUsers
                .AnyAsync(x =>
                    x.ServiceProviderId == serviceProviderId &&
                    x.UserId == userId);
        }

        public async Task<bool>
            UpdateStatusAsync(
                int serviceProviderUserId,
                bool isActive)
        {
            var entity =
                await _context.ServiceProviderUsers
                    .FirstOrDefaultAsync(x =>
                        x.ServiceProviderUserId ==
                        serviceProviderUserId);

            if (entity == null)
                return false;

            entity.IsActive = isActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool>
            UpdatePrimaryContactAsync(
                int serviceProviderUserId,
                bool isPrimaryContact)
        {
            var entity =
                await _context.ServiceProviderUsers
                    .FirstOrDefaultAsync(x =>
                        x.ServiceProviderUserId ==
                        serviceProviderUserId);

            if (entity == null)
                return false;

            entity.IsPrimaryContact = isPrimaryContact;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}