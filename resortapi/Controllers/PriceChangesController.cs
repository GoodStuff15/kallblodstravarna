using Microsoft.AspNetCore.Mvc;
using resortapi.Services;
using resortdtos;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceChangesController : ControllerBase
    {
        private readonly IPriceChangesService _service;

        public PriceChangesController(IPriceChangesService service)
        {
            _service = service;
        }

        [HttpGet("Get all price changes")]
        public async Task<ActionResult<ICollection<PriceChangesDto>>> GetAllPriceChanges()
        {
            var priceChanges = await _service.GetAllAsync();
            if (priceChanges == null || !priceChanges.Any())
                return NotFound("No price changes found.");

            return Ok(priceChanges);
        }

        [HttpGet("{id}", Name = "Get PriceChange by Id")]
        public async Task<ActionResult<PriceChangesDto>> GetPriceChangeById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
                return NotFound($"Price change with Id {id} can not be found");

            return Ok(dto);
        }

        [HttpPost(Name = "Add new price change")]
        public async Task<ActionResult> AddNewPriceChange([FromBody] PriceChangesDto newPriceChange)
        {
            var created = await _service.AddAsync(newPriceChange);
            if (created == null)
                return BadRequest("Invalid price change data.");

            return CreatedAtRoute("Get PriceChange by Id", new { id = created.Id }, created);
        }

        [HttpPut("{id}", Name = "Update PriceChange by Id")]
        public async Task<ActionResult> UpdatePriceChange(int id, [FromBody] PriceChangesDto updatedPriceChange)
        {
            var success = await _service.UpdateAsync(id, updatedPriceChange);
            if (!success)
                return NotFound($"Price change with Id {id} can not be found");

            return Ok($"Price change with Id {id} updated successfully");
        }

        [HttpDelete("{id}", Name = "Delete PriceChange by Id")]
        public async Task<ActionResult> DeletePriceChange(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound($"Price change with Id {id} can not be found");

            return Ok($"Price change with Id {id} deleted successfully");
        }
    }
}
