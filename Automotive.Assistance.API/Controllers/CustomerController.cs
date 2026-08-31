// Automotive.Assistance.API.Controllers/CustomerController.cs
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automotive.Assistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService service, ILogger<CustomerController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Get all customers
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
                _logger.LogError(ex, "Error getting all customers");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all active customers
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
                _logger.LogError(ex, "Error getting active customers");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get customer by ID
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
                _logger.LogError(ex, "Error getting customer by ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get customer by User ID
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try
            {
                var response = await _service.GetByUserIdAsync(userId);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customer by UserId: {UserId}", userId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get customer by Customer Code
        /// </summary>
        [HttpGet("code/{customerCode}")]
        public async Task<IActionResult> GetByCustomerCode(string customerCode)
        {
            try
            {
                var response = await _service.GetByCustomerCodeAsync(customerCode);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customer by CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = response.CustomerId }, response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing customer
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerUpdateDTO dto)
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
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer with ID: {Id}", dto.CustomerId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a customer (hard delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return Ok(new { message = "Customer deleted successfully", success = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer with ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deactivate a customer (soft delete)
        /// </summary>
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                var result = await _service.SoftDeleteAsync(id);
                return Ok(new { message = "Customer deactivated successfully", success = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating customer with ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Activate a customer
        /// </summary>
        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            try
            {
                var result = await _service.ActivateCustomerAsync(id);
                return Ok(new { message = "Customer activated successfully", success = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating customer with ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Check if customer exists
        /// </summary>
        [HttpGet("exists/{id}")]
        public async Task<IActionResult> Exists(int id)
        {
            try
            {
                var exists = await _service.ExistsAsync(id);
                return Ok(new { exists = exists });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking existence for customer ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get total count of active customers
        /// </summary>
        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var count = await _service.GetTotalCountAsync();
                return Ok(new { totalCount = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customer count");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get paged customers
        /// </summary>
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest("Page number and page size must be greater than 0");

            try
            {
                var response = await _service.GetPagedAsync(pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paged customers");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Search customers
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest("Search term is required");

            try
            {
                var response = await _service.SearchAsync(searchTerm);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching customers");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get customers by date of birth range
        /// </summary>
        [HttpGet("dob-range")]
        public async Task<IActionResult> GetByDateOfBirthRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
                return BadRequest("Start date must be before end date");

            try
            {
                var response = await _service.GetCustomersByDateOfBirthRangeAsync(startDate, endDate);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customers by date of birth range");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}