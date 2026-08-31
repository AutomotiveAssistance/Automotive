// Automotive.Assistance.API.Services/CustomerAddressService.cs
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Models.Entities;
using Automotive.Assistance.API.Mappers;
using Automotive.Assistance.API.Repositories.Interfaces;
using Automotive.Assistance.API.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Automotive.Assistance.API.Services
{
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly ICustomerAddressRepository _repository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerAddressService> _logger;

        public CustomerAddressService(
            ICustomerAddressRepository repository,
            ICustomerRepository customerRepository,
            ILogger<CustomerAddressService> logger)
        {
            _repository = repository;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<CustomerAddressListResponseDTO> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var customerNames = await GetCustomerNamesDictionaryAsync(entities);
                var responses = CustomerAddressMapper.ToResponseDTOList(entities, customerNames);

                return new CustomerAddressListResponseDTO
                {
                    Addresses = responses,
                    TotalCount = responses.Count,
                    PageNumber = 1,
                    PageSize = responses.Count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all customer addresses");
                throw;
            }
        }

        public async Task<CustomerAddressListResponseDTO> GetAllActiveAsync()
        {
            try
            {
                var entities = await _repository.GetAllActiveAsync();
                var customerNames = await GetCustomerNamesDictionaryAsync(entities);
                var responses = CustomerAddressMapper.ToResponseDTOList(entities, customerNames);

                return new CustomerAddressListResponseDTO
                {
                    Addresses = responses,
                    TotalCount = responses.Count,
                    PageNumber = 1,
                    PageSize = responses.Count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting active customer addresses");
                throw;
            }
        }

        public async Task<CustomerAddressResponseDTO> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer address with ID {id} not found");

                var customer = await _customerRepository.GetByIdAsync(entity.CustomerId);
                return CustomerAddressMapper.ToResponseDTO(entity, customer?.CustomerCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer address by ID: {Id}", id);
                throw;
            }
        }

        public async Task<CustomerAddressListResponseDTO> GetByCustomerIdAsync(int customerId)
        {
            try
            {
                var entities = await _repository.GetByCustomerIdAsync(customerId);
                var customer = await _customerRepository.GetByIdAsync(customerId);
                var customerNames = new Dictionary<int, string> { { customerId, customer?.CustomerCode } };
                var responses = CustomerAddressMapper.ToResponseDTOList(entities, customerNames);

                return new CustomerAddressListResponseDTO
                {
                    Addresses = responses,
                    TotalCount = responses.Count,
                    PageNumber = 1,
                    PageSize = responses.Count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting addresses for customer ID: {CustomerId}", customerId);
                throw;
            }
        }

        public async Task<CustomerAddressListResponseDTO> GetActiveAddressesByCustomerIdAsync(int customerId)
        {
            try
            {
                var entities = await _repository.GetActiveAddressesByCustomerIdAsync(customerId);
                var customer = await _customerRepository.GetByIdAsync(customerId);
                var customerNames = new Dictionary<int, string> { { customerId, customer?.CustomerCode } };
                var responses = CustomerAddressMapper.ToResponseDTOList(entities, customerNames);

                return new CustomerAddressListResponseDTO
                {
                    Addresses = responses,
                    TotalCount = responses.Count,
                    PageNumber = 1,
                    PageSize = responses.Count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting active addresses for customer ID: {CustomerId}", customerId);
                throw;
            }
        }

        public async Task<CustomerAddressResponseDTO> GetDefaultAddressAsync(int customerId)
        {
            try
            {
                var entity = await _repository.GetDefaultAddressByCustomerIdAsync(customerId);
                if (entity == null)
                    throw new KeyNotFoundException($"Default address not found for customer ID {customerId}");

                var customer = await _customerRepository.GetByIdAsync(customerId);
                return CustomerAddressMapper.ToResponseDTO(entity, customer?.CustomerCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting default address for customer ID: {CustomerId}", customerId);
                throw;
            }
        }

        public async Task<CustomerAddressResponseDTO> CreateAsync(CustomerAddressCreateDTO dto)
        {
            try
            {
                // Check if customer exists
                var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);
                if (customer == null)
                    throw new KeyNotFoundException($"Customer with ID {dto.CustomerId} not found");

                // If this address is default, remove default flag from other addresses
                if (dto.IsDefault)
                {
                    await _repository.RemoveDefaultFlagForCustomerAsync(dto.CustomerId);
                }
                // If it's the first address, make it default
                else
                {
                    var addressCount = await _repository.GetAddressCountByCustomerIdAsync(dto.CustomerId);
                    if (addressCount == 0)
                    {
                        dto.IsDefault = true;
                    }
                }

                var entity = CustomerAddressMapper.ToEntity(dto);
                var createdEntity = await _repository.CreateAsync(entity);

                return CustomerAddressMapper.ToResponseDTO(createdEntity, customer?.CustomerCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating customer address");
                throw;
            }
        }

        public async Task<CustomerAddressResponseDTO> UpdateAsync(CustomerAddressUpdateDTO dto)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(dto.CustomerAddressId);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer address with ID {dto.CustomerAddressId} not found");

                // Check if customer exists
                var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);
                if (customer == null)
                    throw new KeyNotFoundException($"Customer with ID {dto.CustomerId} not found");

                // If setting as default, remove default flag from other addresses
                if (dto.IsDefault && !entity.IsDefault)
                {
                    await _repository.RemoveDefaultFlagForCustomerAsync(dto.CustomerId, dto.CustomerAddressId);
                }

                CustomerAddressMapper.UpdateEntity(dto, entity);
                var updatedEntity = await _repository.UpdateAsync(entity);

                return CustomerAddressMapper.ToResponseDTO(updatedEntity, customer?.CustomerCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating customer address with ID: {Id}", dto.CustomerAddressId);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer address with ID {id} not found");

                // If deleting default address, set another address as default
                if (entity.IsDefault)
                {
                    var otherAddresses = await _repository.GetActiveAddressesByCustomerIdAsync(entity.CustomerId);
                    var nextAddress = otherAddresses.FirstOrDefault(x => x.CustomerAddressId != id);
                    if (nextAddress != null)
                    {
                        nextAddress.IsDefault = true;
                        await _repository.UpdateAsync(nextAddress);
                    }
                }

                var result = await _repository.DeleteAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting customer address with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer address with ID {id} not found");

                // If deactivating default address, set another address as default
                if (entity.IsDefault)
                {
                    var otherAddresses = await _repository.GetActiveAddressesByCustomerIdAsync(entity.CustomerId);
                    var nextAddress = otherAddresses.FirstOrDefault(x => x.CustomerAddressId != id);
                    if (nextAddress != null)
                    {
                        nextAddress.IsDefault = true;
                        await _repository.UpdateAsync(nextAddress);
                    }
                }

                var result = await _repository.SoftDeleteAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft deleting customer address with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ActivateAddressAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer address with ID {id} not found");

                var result = await _repository.UpdateStatusAsync(id, true);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while activating customer address with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeactivateAddressAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer address with ID {id} not found");

                // If deactivating default address, set another address as default
                if (entity.IsDefault)
                {
                    var otherAddresses = await _repository.GetActiveAddressesByCustomerIdAsync(entity.CustomerId);
                    var nextAddress = otherAddresses.FirstOrDefault(x => x.CustomerAddressId != id);
                    if (nextAddress != null)
                    {
                        nextAddress.IsDefault = true;
                        await _repository.UpdateAsync(nextAddress);
                    }
                }

                var result = await _repository.UpdateStatusAsync(id, false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deactivating customer address with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> SetDefaultAddressAsync(int customerId, int addressId)
        {
            try
            {
                var address = await _repository.GetByIdAsync(addressId);
                if (address == null)
                    throw new KeyNotFoundException($"Customer address with ID {addressId} not found");

                if (address.CustomerId != customerId)
                    throw new InvalidOperationException($"Address {addressId} does not belong to customer {customerId}");

                // Remove default flag from all addresses
                await _repository.RemoveDefaultFlagForCustomerAsync(customerId);

                // Set this address as default
                address.IsDefault = true;
                address.UpdatedAt = DateTime.UtcNow;
                address.UpdatedBy = "SYSTEM";

                await _repository.UpdateAsync(address);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while setting default address for customer ID: {CustomerId}, address ID: {AddressId}", customerId, addressId);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                return await _repository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence for address ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> HasDefaultAddressAsync(int customerId)
        {
            try
            {
                return await _repository.HasDefaultAddressAsync(customerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking default address for customer ID: {CustomerId}", customerId);
                throw;
            }
        }

        public async Task<int> GetAddressCountByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _repository.GetAddressCountByCustomerIdAsync(customerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting address count for customer ID: {CustomerId}", customerId);
                throw;
            }
        }

        // Helper methods
        private async Task<Dictionary<int, string>> GetCustomerNamesDictionaryAsync(List<CustomerAddress> addresses)
        {
            if (!addresses.Any())
                return new Dictionary<int, string>();

            var customerIds = addresses.Select(a => a.CustomerId).Distinct().ToList();
            var customerNames = new Dictionary<int, string>();
            return customerNames;
            
        }
    }
}