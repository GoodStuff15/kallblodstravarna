using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resortapi.Converters;
using resortapi.Data;
using resortapi.Repositories;
using resortapi.Services;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccomodationController : ControllerBase
    {
        private readonly IAccomodationService _service;
        public AccomodationController(IAccomodationService service)
        {
            _service = service;
        }


        // [FromBody] kan/bör ändras till [FromQuery] om vi vill skicka in datumen som query-parametrar istället för i body

        //[Authorize(Roles = "Staff, Admin")]
        [HttpGet("availableReceptionist")]
        public async Task<ActionResult<ICollection<AvailableRoomDto>>> GetAvailableAccomodations(
            [FromQuery] DateTime checkIn,
            [FromQuery] DateTime checkOut,
            [FromQuery] int noOfGuests)
        {
            return Ok(_service.GetAvailableRooms(new AvailableRoomRequest
            {
                CheckIn = checkIn,
                CheckOut = checkOut,
                NoOfGuests = noOfGuests
            }));

        }

        [HttpGet("availableGuest")]
        public async Task<ActionResult<ICollection<AvailableRoomDto>>> GetAvailableAccomodationsExclGuests([FromBody] AvailableRoomRequestExclGuests request)
        {
            return Ok(_service.GetAvailableGuest(request));
        }

        //[Authorize(Roles = "Staff, Admin")]
        [HttpGet("Get all Accomodations")] // all accomodations available/not available
        public async Task<ActionResult<ICollection<AvailableRoomDto>>> GetAllAccomodations()
        {
            var accomodations = await _service.GetAllAccomodations();
            return Ok(accomodations);
        }

        [HttpGet("{id}", Name = "Get Accomodation by Id")]
        public async Task<ActionResult<AvailableRoomDto>> GetAccomodationById(int id)
        {
            var accomodation = await _service.GetAccomodationById(id);
            return Ok(accomodation);
        }

        //[Authorize(Roles = "Staff, Admin")]
        [HttpPost(Name = "Add New Accomodation")]
        public async Task<ActionResult> AddNewAccomodation([FromBody] AccomodationDto newAccomodation)
        {
            var newAcc = await _service.AddAccomodation(newAccomodation);
            return CreatedAtRoute("Get Accomodation by Id", new { id = newAcc.Id }, newAcc);
        }

        [HttpPut("{id}", Name = "Update Accomodation")]
        public async Task<ActionResult> UpdateAccomodation(int id, [FromBody] AccomodationDto updatedAccomodation)
        {
            var updated = await _service.UpdateAccomodation(id, updatedAccomodation);
            return Ok(updated);
        }

        [HttpDelete("{id}", Name = "Delete Accomodation")]
        public async Task<ActionResult> DeleteAccomodation(int id)
        {
            var deleted = await _service.DeleteAccomodation(id);
            return Ok($"Accomodation {id} deleted successfully");
        }

    }
}
