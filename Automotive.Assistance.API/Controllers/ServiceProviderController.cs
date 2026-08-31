using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Automotive.Assistance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceProviderController : ControllerBase
    {
        private readonly IServiceProviderService _serviceProviderService;
        private readonly ILogger<ServiceProviderController> _logger;

        public ServiceProviderController(IServiceProviderService serviceProviderService,
            ILogger<ServiceProviderController> logger)
        {
            _serviceProviderService = serviceProviderService;
            _logger = logger;
        }

        // =====================================================
        // GET ALL SERVICE PROVIDERS
        // GET: api/ServiceProvider
        // =====================================================

        [HttpGet]
        public async Task<ActionResult> GetServiceProvidersListAsync()
        {
            try
            {
                var serviceProviders =
                    await _serviceProviderService.GetServiceProvidersListAsync();

                return Ok(serviceProviders);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Getting error while fetching service providers");

                return new StatusCodeResult(
                    StatusCodes.Status500InternalServerError);
            }
        }


        // =====================================================
        // GET SERVICE PROVIDER BY ID
        // GET: api/ServiceProvider/5
        // =====================================================

        [HttpGet("{serviceProviderId}")]
        public async Task<ActionResult> GetServiceProviderByIdAsync(
            int serviceProviderId)
        {
            try
            {
                var serviceProvider =
                    await _serviceProviderService
                        .GetServiceProviderByIdAsync(serviceProviderId);

                if (serviceProvider == null)
                {
                    return NotFound(new
                    {
                        Message = "Service provider not found."
                    });
                }

                return Ok(serviceProvider);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Getting error while fetching service provider. Id: {ServiceProviderId}",
                    serviceProviderId);

                return new StatusCodeResult(
                    StatusCodes.Status500InternalServerError);
            }
        }


        // =====================================================
        // CREATE SERVICE PROVIDER
        // POST: api/ServiceProvider
        // =====================================================

        [HttpPost]
        public async Task<ActionResult> CreateServiceProviderAsync([FromBody] ServiceProviderCreateDTO serviceProviderCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var serviceProvider = await _serviceProviderService.CreateServiceProviderAsync(serviceProviderCreateDTO);

                   return CreatedAtAction(nameof(GetServiceProviderByIdAsync),
                    new
                    {
                        serviceProviderId =
                            serviceProvider.ServiceProviderId
                    },
                    serviceProvider);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Validation error while creating service provider");

                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Getting error while creating service provider");

                return new StatusCodeResult(
                    StatusCodes.Status500InternalServerError);
            }
        }


        // =====================================================
        // UPDATE SERVICE PROVIDER
        // PUT: api/ServiceProvider/5
        // =====================================================

        [HttpPut("{serviceProviderId}")]
        public async Task<ActionResult> UpdateServiceProviderAsync(
            int serviceProviderId,
            [FromBody] ServiceProviderUpdateDTO serviceProviderUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var serviceProvider =
                    await _serviceProviderService
                        .UpdateServiceProviderAsync(
                            serviceProviderId,
                            serviceProviderUpdateDTO);

                if (serviceProvider == null)
                {
                    return NotFound(new
                    {
                        Message = "Service provider not found."
                    });
                }

                return Ok(serviceProvider);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Getting error while updating service provider. Id: {ServiceProviderId}",
                    serviceProviderId);

                return new StatusCodeResult(
                    StatusCodes.Status500InternalServerError);
            }
        }


        // =====================================================
        // DELETE SERVICE PROVIDER
        // DELETE: api/ServiceProvider/5
        // =====================================================

        [HttpDelete("{serviceProviderId}")]
        public async Task<ActionResult> DeleteServiceProviderAsync(
            int serviceProviderId)
        {
            try
            {
                var result =
                    await _serviceProviderService
                        .DeleteServiceProviderAsync(serviceProviderId);

                if (!result)
                {
                    return NotFound(new
                    {
                        Message = "Service provider not found."
                    });
                }

                return Ok(new
                {
                    Message = "Service provider deleted successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Getting error while deleting service provider. Id: {ServiceProviderId}",
                    serviceProviderId);

                return new StatusCodeResult(
                    StatusCodes.Status500InternalServerError);
            }
        }


        // =====================================================
        // ACTIVATE / DEACTIVATE SERVICE PROVIDER
        // PATCH: api/ServiceProvider/5/status
        // =====================================================

        [HttpPatch("{serviceProviderId}/status")]
        public async Task<ActionResult> UpdateServiceProviderStatusAsync(
            int serviceProviderId,
            [FromQuery] bool isActive)
        {
            try
            {
                var result =
                    await _serviceProviderService
                        .UpdateServiceProviderStatusAsync(
                            serviceProviderId,
                            isActive);

                if (!result)
                {
                    return NotFound(new
                    {
                        Message = "Service provider not found."
                    });
                }

                return Ok(new
                {
                    Message = isActive
                        ? "Service provider activated successfully."
                        : "Service provider deactivated successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Getting error while updating service provider status. Id: {ServiceProviderId}",
                    serviceProviderId);

                return new StatusCodeResult(
                    StatusCodes.Status500InternalServerError);
            }
        }


        // =====================================================
        // VERIFY / UNVERIFY SERVICE PROVIDER
        // PATCH: api/ServiceProvider/5/verify
        // =====================================================

        [HttpPatch("{serviceProviderId}/verify")]
        public async Task<ActionResult> VerifyServiceProviderAsync(
            int serviceProviderId,
            [FromQuery] bool isVerified)
        {
            try
            {
                var result =
                    await _serviceProviderService
                        .VerifyServiceProviderAsync(
                            serviceProviderId,
                            isVerified);

                if (!result)
                {
                    return NotFound(new
                    {
                        Message = "Service provider not found."
                    });
                }

                return Ok(new
                {
                    Message = isVerified
                        ? "Service provider verified successfully."
                        : "Service provider verification removed successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Getting error while verifying service provider. Id: {ServiceProviderId}",
                    serviceProviderId);

                return new StatusCodeResult(
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
