using Microsoft.AspNetCore.Mvc;
using resortapi.Services;
using resortdtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccomodationTypeController : ControllerBase
    {
        private readonly IAccomodationTypeService _service;

        public AccomodationTypeController(IAccomodationTypeService service)
        {
            _service = service;
        }

        [HttpGet("Get all accomodationType")]
        public async Task<ActionResult<ICollection<AccomodationTypeDto>>> GetAllAccomodationTypes()
        {
            var accomodationTypes = await _service.GetAllAsync();
            if (accomodationTypes == null || accomodationTypes.Count == 0)
            {
                return NotFound("No accomodation types found.");
            }
            return Ok(accomodationTypes);
        }

        [HttpGet("{id}", Name = "Get AccomodationType by Id")]
        public async Task<ActionResult<AccomodationTypeDto>> GetAccomodationTypeById(int id)
        {
            var accomodationType = await _service.GetByIdAsync(id);
            if (accomodationType == null)
            {
                return NotFound($"Accomodation type with Id {id} can not be found");
            }
            return Ok(accomodationType);
        }

        [HttpPost(Name = "Add new accomodationType")]
        public async Task<ActionResult> AddNewAccomodationType([FromBody] AccomodationTypeDto newAccomodationType)
        {
            var added = await _service.AddAsync(newAccomodationType);
            return CreatedAtRoute("Get AccomodationType by Id", new { id = added.Id }, added);
        }

        [HttpPut("{id}", Name = "Update AccomodationType by Id")]
        public async Task<ActionResult> UpdateAccomodationType(int id, [FromBody] AccomodationTypeDto updatedAccomodationType)
        {
            if (id != updatedAccomodationType.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var updated = await _service.UpdateAsync(id, updatedAccomodationType);
            if (!updated)
            {
                return NotFound($"Accomodation type with Id {id} does not exist.");
            }
            return Ok($"AccomodationType {id} updated successfully");
        }

        [HttpDelete("{id}", Name = "Delete AccomodationType by Id")]
        public async Task<ActionResult> DeleteAccomodationType(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound($"Accomodation type with Id {id} does not exist.");
            }
            return Ok($"AccomodationType {id} deleted successfully");
        }
    }
}
