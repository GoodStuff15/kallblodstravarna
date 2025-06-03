using Microsoft.AspNetCore.Mvc;
using resortapi.Services;
using resortdtos;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessibilityController : ControllerBase
    {
        private readonly IAccessibilityService _service;

        public AccessibilityController(IAccessibilityService service)
        {
            _service = service;
        }

        [HttpGet("Get all accessibilities")]
        public async Task<ActionResult<ICollection<AccessibilityDto>>> GetAllAccessibilities()
        {
            var accessibilities = await _service.GetAllAccessibilitiesAsync();
            if (accessibilities == null || !accessibilities.Any())
            {
                return NotFound("No accessibilities found.");
            }

            return Ok(accessibilities);
        }

        [HttpGet("{id}", Name = "Get Accessibility by Id")]
        public async Task<ActionResult<AccessibilityDto>> GetAccessibilityById(int id)
        {
            var dto = await _service.GetAccessibilityByIdAsync(id);
            if (dto == null)
            {
                return NotFound($"Accessibility with Id {id} can not be found");
            }

            return Ok(dto);
        }

        [HttpPost(Name = "Add new accessibility")]
        public async Task<ActionResult> AddNewAccessibility([FromBody] AccessibilityDto newAccessibility)
        {
            var created = await _service.AddNewAccessibilityAsync(newAccessibility);
            if (created == null)
            {
                return BadRequest("Invalid accessibility data.");
            }

            return CreatedAtRoute("Get Accessibility by Id", new { id = created.Id }, created);
        }

        [HttpPut("{id}", Name = "Update Accessibility by Id")]
        public async Task<ActionResult> UpdateAccessibility(int id, [FromBody] AccessibilityDto updatedAccessibility)
        {
            var success = await _service.UpdateAccessibilityAsync(id, updatedAccessibility);
            if (!success)
            {
                return NotFound($"Accessibility with Id {id} can not be found");
            }

            return Ok($"Accessibility with Id {id} has been updated");
        }

        [HttpDelete("{id}", Name = "Delete Accessibility by Id")]
        public async Task<ActionResult> DeleteAccessibility(int id)
        {
            var success = await _service.DeleteAccessibilityAsync(id);
            if (!success)
            {
                return NotFound($"Accessibility with Id {id} can not be found");
            }

            return Ok($"Accessibility with Id {id} has been deleted");
        }
    }
}
