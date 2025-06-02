using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessibilityController : ControllerBase
    {
        private readonly AccessibilityRepo _repo;
        private readonly AccessibilityConverter _converter;
        public AccessibilityController(AccessibilityRepo accessibilityRepo, AccessibilityConverter accessibilityConverter)
        {
            _repo = accessibilityRepo;
            _converter = accessibilityConverter;
        }
        [HttpGet("Get all accessibilities")]
        public async Task<ActionResult<ICollection<AccessibilityDto>>> GetAllAccessibilities()
        {
            var accessibilities = await _repo.GetAllAsync();
            if (accessibilities == null || !accessibilities.Any())
            {
                return NotFound("No accessibilities found.");
            }
            var available = _converter.FromObjecttoDTO_Collection(accessibilities);
            return Ok(available);
        }
        [HttpGet("{id}", Name = "Get Accessibility by Id")]
        public async Task<ActionResult<AccessibilityDto>> GetAccessibilityById(int id)
        {
            var accessibility = await _repo.GetByIdAsync(id);
            if (accessibility == null)
            {
                return NotFound($"Accessibility with Id {id} can not be found");
            }
            var dto = _converter.FromObjecttoDTO(accessibility);
            return Ok(dto);
        }
        [HttpPost(Name = "Add new accessibility")]
        public async Task<ActionResult> AddNewAccessibility([FromBody] AccessibilityDto newAccessibility)
        {
            var accessibility = _converter.FromDTOtoObject(newAccessibility);
            if (accessibility == null)
            {
                return BadRequest("Invalid accessibility data.");
            }
            accessibility = await _repo.AddAsync(accessibility);
            var newAcc = _converter.FromObjecttoDTO(accessibility);
            return CreatedAtRoute("Get Accessibility by Id", new { id = newAcc.Id }, newAcc);
        }
        [HttpPut("{id}", Name = "Update Accessibility by Id")]
        public async Task<ActionResult> UpdateAccessibility(int id, [FromBody] AccessibilityDto updatedAccessibility)
        {
            var existingAccessibility = await _repo.GetByIdAsync(id);
            if (existingAccessibility == null)
            {
                return NotFound($"Accessibility with Id {id} can not be found");
            }
            existingAccessibility.Name = updatedAccessibility.Name;
            existingAccessibility.Description = updatedAccessibility.Description;
            existingAccessibility.Accomodations.Clear();
            //foreach (var accomodationId in updatedAccessibility.AccomodationIds)
            //{
            //    var accomodation = await _repo.GetAccomodationByIdAsync(accomodationId);
            //    if (accomodation != null)
            //    {
            //        existingAccessibility.Accomodations.Add(accomodation);
            //    }
            //}
            await _repo.UpdateAsync(existingAccessibility);
            return Ok($"Accessibility with Id {id} has been updated");
        }
    }
}
