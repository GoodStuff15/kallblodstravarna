using Microsoft.AspNetCore.Mvc;
using resortapi.Services;
using resortdtos;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalOptionController : ControllerBase
    {
        private readonly IAdditionalOptionService _service;

        public AdditionalOptionController(IAdditionalOptionService service)
        {
            _service = service;
        }

        [HttpGet(Name = "Get all additional options")]
        public async Task<ActionResult<ICollection<AdditionalOptionDto>>> GetAll()
        {
            var options = await _service.GetAllAsync();
            if (!options.Any())
                return NoContent();

            return Ok(options);
        }

        [HttpGet("{id}", Name = "Get AdditionalOption by Id")]
        public async Task<ActionResult<AdditionalOptionDto>> GetById(int id)
        {
            var option = await _service.GetByIdAsync(id);
            if (option == null)
                return NotFound($"AdditionalOption with Id {id} not found");

            return Ok(option);
        }

        [HttpPost(Name = "Add new additional option")]
        public async Task<ActionResult> Add([FromBody] AdditionalOptionDto dto)
        {
            var added = await _service.AddAsync(dto);
            return CreatedAtRoute("Get AdditionalOption by Id", new { id = added.Id }, added);
        }

        [HttpPut("{id}", Name = "Update AdditionalOption by Id")]
        public async Task<ActionResult> Update(int id, [FromBody] AdditionalOptionDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated)
                return NotFound($"AdditionalOption with Id {id} not found");

            return Ok($"AdditionalOption with Id {id} updated successfully");
        }

        [HttpDelete("{id}", Name = "Delete AdditionalOption by Id")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound($"AdditionalOption with Id {id} not found");

            return Ok($"AdditionalOption with Id {id} deleted successfully");
        }
    }
}
