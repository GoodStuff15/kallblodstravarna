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
    }
}
