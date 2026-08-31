// Automotive.Assistance.API.Controllers/CustomerAddressController.cs
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automotive.Assistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly ICustomerAddressService _service;
        private readonly ILogger<CustomerAddressController> _logger;

        public CustomerAddressController(
            ICustomerAddressService service,
            ILogger<CustomerAddressController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Get all customer addresses
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _service.GetAllAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all customer addresses");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all active customer addresses
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            try
            {
                var response = await _service.GetAllActiveAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active customer addresses");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get customer address by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customer address by ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all addresses for a customer
        /// </summary>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            try
            {
                var response = await _service.GetByCustomerIdAsync(customerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting addresses for customer ID: {CustomerId}", customerId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get active addresses for a customer
        /// </summary>
        [HttpGet("customer/{customerId}/active")]
        public async Task<IActionResult> GetActiveAddressesByCustomerId(int customerId)
        {
            try
            {
                var response = await _service.GetActiveAddressesByCustomerIdAsync(customerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active addresses for customer ID: {CustomerId}", customerId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get default address for a customer
        /// </summary>
        [HttpGet("customer/{customerId}/default")]
        public async Task<IActionResult> GetDefaultAddress(int customerId)
        {
            try
            {
                var response = await _service.GetDefaultAddressAsync(customerId);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting default address for customer ID: {CustomerId}", customerId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new customer address
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerAddressCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = response.CustomerAddressId }, response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer address");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing customer address
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerAddressUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _service.UpdateAsync(dto);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer address with ID: {Id}", dto.CustomerAddressId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a customer address (hard delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return Ok(new { message = "Customer address deleted successfully", success = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer address with ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deactivate a customer address (soft delete)
        /// </summary>
    }
}