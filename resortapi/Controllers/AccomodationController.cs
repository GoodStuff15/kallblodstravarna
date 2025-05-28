using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using resortapi.Repositories;
using resortdtos;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccomodationController : ControllerBase
    {
        private readonly AccomodationRepo _repo;
        public AccomodationController(AccomodationRepo accomodationRepo)
        {
            _repo = accomodationRepo;
        }

        // [FromBody] kan/bör ändras till [FromQuery] om vi vill skicka in datumen som query-parametrar istället för i body

        [Authorize(Roles = "Staff, Admin")]
        [HttpGet("availableReceptionist")]
        public async Task<ActionResult<ICollection<AvailableRoomDto>>> GetAvailableAccomodations([FromBody] AvailableRoomRequest request)
        {
            var accomodations = await _repo.GetAvailableByGuestNo(request.CheckIn, request.CheckOut, request.NoOfGuests);

            var available = accomodations.Select(a => new AvailableRoomDto
            {
                Id = a.Id,
                Name = a.Name,
                AccomodationType = a.AccomodationType.Name,
                Description = a.AccomodationType.Description,
                MaxOccupancy = a.MaxOccupancy,
                BasePrice = a.AccomodationType.BasePrice,
                Accessibility = a.Accessibilities?.Select(acc => new AccessibilityDto
                {
                    Name = acc.Name,
                    Description = acc.Description
                }).ToList() ?? new List<AccessibilityDto>()
            }).ToList();

            return Ok(available);
        }

        [HttpGet("availableGuest")]
        public async Task<ActionResult<ICollection<AvailableRoomDto>>> GetAvailableAccomodationsExclGuests([FromBody] AvailableRoomRequestExclGuests request)
        {
            var accomodations = await _repo.GetAvailableAsync(request.CheckIn, request.CheckOut);

            var available = accomodations.Select(a => new AvailableRoomDto
            {
                Id = a.Id,
                Name = a.Name,
                AccomodationType = a.AccomodationType.Name,
                Description = a.AccomodationType.Description,
                MaxOccupancy = a.MaxOccupancy,
                BasePrice = a.AccomodationType.BasePrice,
                Accessibility = a.Accessibilities?.Select(acc => new AccessibilityDto
                {
                    Name = acc.Name,
                    Description = acc.Description
                }).ToList() ?? new List<AccessibilityDto>()
            }).ToList();

            return Ok(available);
        }


    }
}
