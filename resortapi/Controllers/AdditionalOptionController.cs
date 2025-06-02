using Microsoft.AspNetCore.Mvc;
using resortapi.Repositories;
using resortdtos;
using resortapi.Converters;
using resortlibrary.Models;
using Microsoft.AspNetCore.Authorization;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalOptionController : ControllerBase
    {
        private readonly IRepository<AdditionalOption> _repo;
        public AdditionalOptionController(IRepository<AdditionalOption> repo)
        {
            _repo = repo;
        }

        [HttpGet(Name = "Get all additional options")]
        public async Task<ActionResult<ICollection<AdditionalOption>>> GetAll()
        {
            var additinaloptions = await _repo.GetAllWithIncludesAsync();

            if (!additinaloptions.Any())
            {
                return NoContent();
            }

            return Ok(additinaloptions);

        }

    }
}