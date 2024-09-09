using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;

namespace Zavrsni_rad_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplyController : ControllerBase
    {
        private readonly ISupplyService _supplyService;

        public SupplyController(ISupplyService supplyService)
        {
            _supplyService = supplyService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllSupplies()
        {
            var supplies = await _supplyService.GetAllSuppliesAsync();
            return Ok(supplies);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetSupply(int id)
        {
            var supply = await _supplyService.GetSupplyByIdAsync(id);
            if (supply == null)
            {
                return NotFound();
            }
            return Ok(supply);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSupply([FromBody] SupplyDto supplyDto)
        {
            await _supplyService.AddSupplyAsync(supplyDto);
            return CreatedAtAction(nameof(GetSupply), new { id = supplyDto.Id }, supplyDto);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSupply(int id, [FromBody] SupplyDto supplyDto)
        {
            if (id != supplyDto.Id)
            {
                return BadRequest();
            }
            await _supplyService.UpdateSupplyAsync(supplyDto);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSupply(int id)
        {
            await _supplyService.DeleteSupplyAsync(id);
            return NoContent();
        }
    }
}
