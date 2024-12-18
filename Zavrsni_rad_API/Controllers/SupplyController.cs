using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.ServiceModels;
using ServiceLayer.Services.Abstraction;
using ServiceLayer.Services.Implementation;

namespace Zavrsni_rad_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SupplyController : ControllerBase
    {
        private readonly ISupplyService _supplyService;

        public SupplyController(ISupplyService supplyService)
        {
            _supplyService = supplyService;
        }

        [HttpGet("GetAllSupplies")]
        public async Task<IActionResult> GetAllSupplies()
        {
            var supplies = await _supplyService.GetAllSuppliesAsync();
            return Ok(supplies);
        }

        [HttpGet("GetSupply/{id}")]
        public async Task<IActionResult> GetSupply(int id)
        {
            var supply = await _supplyService.GetSupplyByIdAsync(id);
            if (supply == null)
            {
                return NotFound();
            }
            return Ok(supply);
        }

        [HttpPost("CreateSupply")]
        public async Task<IActionResult> CreateSupply([FromBody] SupplyCreateRequest supplyDto)
        {
            try
            {
                await _supplyService.AddSupplyAsync(supplyDto);
                return CreatedAtAction(nameof(GetSupply), new { id = supplyDto.Id }, supplyDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost("UpdateSupply")]
        public async Task<IActionResult> UpdateSupply([FromBody] SupplyCreateRequest supplyDto)
        {
            try
            {
                if (supplyDto.Id <= 0)
                {
                    return BadRequest("Invalid ID.");
                }
                var supply = await _supplyService.GetSupplyByIdAsync(supplyDto.Id);
                if (supply == null)
                {
                    return NotFound();
                }
                await _supplyService.UpdateSupplyAsync(supplyDto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteSupply/{id}")]
        public async Task<IActionResult> DeleteSupply(int id)
        {
            await _supplyService.DeleteSupplyAsync(id);
            return NoContent();
        }
    }
}
