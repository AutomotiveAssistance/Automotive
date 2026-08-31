using Automotive.Assistance.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Automotive.Assistance.API.Repositories
{
    public class ServiceProviderRepository : IServiceProviderRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Automotive.Assistance.API.Models.Entities.ServiceProvider>> GetServiceProvidersListAsync()
        {
            return await _context.ServiceProviders
                .AsNoTracking()
                .OrderBy(x => x.ServiceProviderName)
                .ToListAsync();
        }

        public async Task<Automotive.Assistance.API.Models.Entities.ServiceProvider?> GetServiceProviderByIdAsync(int serviceProviderId)
        {
            return await _context.ServiceProviders
                .FirstOrDefaultAsync(x =>
                    x.ServiceProviderId == serviceProviderId);
        }

        public async Task<Automotive.Assistance.API.Models.Entities.ServiceProvider?> GetServiceProviderByCodeAsync(
            string serviceProviderCode)
        {
            return await _context.ServiceProviders
                .FirstOrDefaultAsync(x =>
                    x.ServiceProviderCode == serviceProviderCode);
        }

        public async Task<Automotive.Assistance.API.Models.Entities.ServiceProvider?> GetServiceProviderByNameAsync(
            string serviceProviderName)
        {
            return await _context.ServiceProviders
                .FirstOrDefaultAsync(x =>
                    x.ServiceProviderName == serviceProviderName);
        }

        public async Task<Automotive.Assistance.API.Models.Entities.ServiceProvider?> CreateServiceProviderAsync(Automotive.Assistance.API.Models.Entities.ServiceProvider serviceProvider)
        {
            await _context.ServiceProviders.AddAsync(serviceProvider);

            await _context.SaveChangesAsync();

            return serviceProvider;
        }

        public async Task<Automotive.Assistance.API.Models.Entities.ServiceProvider> UpdateServiceProviderAsync(
            Automotive.Assistance.API.Models.Entities.ServiceProvider serviceProvider)
        {
            _context.ServiceProviders.Update(serviceProvider);

            await _context.SaveChangesAsync();

            return serviceProvider;
        }

        public async Task<bool> DeleteServiceProviderAsync(
            int serviceProviderId)
        {
            var serviceProvider = await _context.ServiceProviders
                .FirstOrDefaultAsync(x =>
                    x.ServiceProviderId == serviceProviderId);

            if (serviceProvider == null)
                return false;

            _context.ServiceProviders.Remove(serviceProvider);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ServiceProviderExistsAsync(
            int serviceProviderId)
        {
            return await _context.ServiceProviders
                .AnyAsync(x =>
                    x.ServiceProviderId == serviceProviderId);
        }

        public async Task<bool> ServiceProviderCodeExistsAsync(
            string serviceProviderCode)
        {
            return await _context.ServiceProviders
                .AnyAsync(x =>
                    x.ServiceProviderCode == serviceProviderCode);
        }

        public async Task<bool> ServiceProviderNameExistsAsync(
            string serviceProviderName)
        {
            return await _context.ServiceProviders
                .AnyAsync(x =>
                    x.ServiceProviderName == serviceProviderName);
        }

        public async Task<bool> UpdateServiceProviderStatusAsync(
            int serviceProviderId,
            bool isActive)
        {
            var serviceProvider = await _context.ServiceProviders
                .FirstOrDefaultAsync(x =>
                    x.ServiceProviderId == serviceProviderId);

            if (serviceProvider == null)
                return false;

            serviceProvider.IsActive = isActive;
            serviceProvider.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> VerifyServiceProviderAsync(
            int serviceProviderId,
            bool isVerified)
        {
            var serviceProvider = await _context.ServiceProviders
                .FirstOrDefaultAsync(x =>
                    x.ServiceProviderId == serviceProviderId);

            if (serviceProvider == null)
                return false;

            serviceProvider.IsVerified = isVerified;
            serviceProvider.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}