using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccomodationTypeController : ControllerBase
    {
        private readonly AccomodationTypeRepo _repo;
        private readonly AccomodationTypeConverter _converter;

        public AccomodationTypeController(AccomodationTypeRepo accomodationTypeRepo, AccomodationTypeConverter accomodationTypeConverter)
        {
            _repo = accomodationTypeRepo;
            _converter = accomodationTypeConverter;
        }

        [HttpGet("Get all accomodationType")]
        public async Task<ActionResult<ICollection<AccomodationTypeDto>>> GetAllAccomodationTypes()
        {
            var accomodationTypes = await _repo.GetAllAsync();
            if (accomodationTypes == null || !accomodationTypes.Any())
            {
                return NotFound("No accomodation types found.");
            }
            var available = _converter.FromObjectCollection_ToOverviewCollection(accomodationTypes);
            return Ok(available);
        }
    }
}
