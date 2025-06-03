using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceChangesController : ControllerBase
    {
        private readonly PriceChangesRepo _repo;
        private readonly PriceChangesConverter _converter;
        public PriceChangesController(PriceChangesRepo priceChangesRepo, PriceChangesConverter priceChangesConverter)
        {
            _repo = priceChangesRepo;
            _converter = priceChangesConverter;
        }
        [HttpGet("Get all price changes")]
        public async Task<ActionResult<ICollection<PriceChangesDto>>> GetAllPriceChanges()
        {
            var priceChanges = await _repo.GetAllAsync();
            if (priceChanges == null || !priceChanges.Any())
            {
                return NotFound("No price changes found.");
            }
            var available = _converter.FromObjecttoDTO_Collection(priceChanges);
            return Ok(available);
        }
        [HttpGet("{id}", Name = "Get PriceChange by Id")]
        public async Task<ActionResult<PriceChangesDto>> GetPriceChangeById(int id)
        {
            var priceChange = await _repo.GetByIdAsync(id);
            if (priceChange == null)
            {
                return NotFound($"Price change with Id {id} can not be found");
            }
            var dto = _converter.FromObjecttoDTO(priceChange);
            return Ok(dto);
        }
        [HttpPost(Name = "Add new price change")]
        public async Task<ActionResult> AddNewPriceChange([FromBody] PriceChangesDto newPriceChange)
        {
            var priceChange = _converter.FromDTOtoObject(newPriceChange);
            if (priceChange == null)
            {
                return BadRequest("Invalid price change data.");
            }
            priceChange = await _repo.AddAsync(priceChange);
            var newPriceChangeDto = _converter.FromObjecttoDTO(priceChange);
            return CreatedAtRoute("Get PriceChange by Id", new { id = newPriceChangeDto.Id }, newPriceChangeDto);
        }
        [HttpPut("{id}", Name = "Update PriceChange by Id")]
        public async Task<ActionResult> UpdatePriceChange(int id, [FromBody] PriceChangesDto updatedPriceChange)
        {
            var existingPriceChange = await _repo.GetByIdAsync(id);
            if (existingPriceChange == null)
            {
                return NotFound($"Price change with Id {id} can not be found");
            }
            existingPriceChange.PriceChange = updatedPriceChange.PriceChange;
            existingPriceChange.Type = updatedPriceChange.Type;

            existingPriceChange.Id = id;
            await _repo.UpdateAsync(existingPriceChange);
            return Ok($"Price change with Id {id} updated successfully");
        }
        [HttpDelete("{id}", Name = "Delete PriceChange by Id")]
        public async Task<ActionResult> DeletePriceChange(int id)
        {
            var existingPriceChange = await _repo.GetByIdAsync(id);
            if (existingPriceChange == null)
            {
                return NotFound($"Price change with Id {id} can not be found");
            }
            await _repo.DeleteAsync(existingPriceChange);
            return Ok($"Price change with Id {id} deleted successfully");
        }
    }
}
