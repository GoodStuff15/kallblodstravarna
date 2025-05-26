using Microsoft.AspNetCore.Mvc;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccomodationController : ControllerBase
    {
        private readonly AccomodationRepo _accomodationRepo;
        public AccomodationController(AccomodationRepo accomodationRepo)
        {
            _accomodationRepo = accomodationRepo;
        }

        [HttpPost("available")]
        public async Task<ActionResult<ICollection<Accomodation>>> GetAvailableAccomodations([FromBody] AvailableRoomRequest request)
        {
            var result = await _accomodationRepo.GetAvailableByGuestNo(request.CheckIn, request.CheckOut, request.NoOfGuests);
            return Ok(result);
        }

    }
}
