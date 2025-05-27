using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

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

        [Authorize(Roles = "Admin")]
        [HttpPost("available")]
        public async Task<ActionResult<ICollection<AvailableRoomDto>>> GetAvailableAccomodations([FromBody] AvailableRoomRequest request)
        {
            var accomodations = await _repo.GetAvailableByGuestNo(request.CheckIn, request.CheckOut, request.NoOfGuests);

            var overview = accomodations.Select(a => new AvailableRoomDto
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


            return Ok(overview);
        }


    }
}
