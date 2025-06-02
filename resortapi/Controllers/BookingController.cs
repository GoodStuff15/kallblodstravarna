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
    public class BookingController : ControllerBase
    {
        private readonly IRepository<Booking> _repo;
        private readonly IBookingRepository _repo2;
        private readonly IBookingConverter _converter;

        public BookingController(IRepository<Booking> repo, IBookingConverter converter, IBookingRepository repo2)
        {
            _repo = repo;
            _repo2 = repo2;
            _converter = converter;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDetailsDto>> GetBooking(int id)
        {
            var booking = await _repo2.GetByIdWithIncludesAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            var dto = _converter.FromObjectToDetailedDTO(booking);
            return Ok(dto);
        }




        [Authorize(Roles = "Staff, Admin")]
        [HttpPost(Name = "Add New Booking")]
        public async Task<ActionResult> AddBooking(BookingDto booking)
        {
            var newBooking = _converter.FromDTOtoObject(booking);

            if (booking == null)
            {
                return BadRequest("Adding booking failed (Booking does not exist)");
            }

            newBooking.TimeOfBooking = DateTime.Now;
            await _repo.CreateAsync(newBooking);

            return Ok($"Booking added to Database successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("overview", Name = "Get bookings overview")]
        public async Task<ActionResult<ICollection<BookingsOverviewDto>>> GetBookingsOverview()
        {
            var bookings = await _repo2.GetAllWithCustomerAsync();

            if (!bookings.Any())
            {
                return NoContent();
            }

            var dtos = _converter.FromObjectCollection_ToOverviewCollection(bookings);

            return Ok(dtos);
        }


        [HttpPut("{cancelById}", Name = "Cancel booking")]
        public async Task<ActionResult> CancelBooking(int cancelById)
        {
            var cancelThis = _repo.GetAsync(cancelById).Result;

            if(cancelThis == null)
            {
                return BadRequest($"Booking with {cancelById} cannot be found!");
            }
            cancelThis.Cancelled = true;
            cancelThis.Active = false;
            await _repo.UpdateAsync(cancelThis);

            return Ok($"Booking #{cancelById} has been cancelled");
        }

        [Authorize(Roles = "Staff, Admin")]
        [HttpDelete("{deleteById}", Name = "Delete booking from database")]
        public async Task<ActionResult> RemoveBookingFromDb(int deleteById)
        {
            var removeThis = _repo.GetAsync(deleteById).Result;
            if (removeThis == null)
            {
                return BadRequest($"Booking with {deleteById} cannot be found!");
            }
            await _repo.DeleteAsync(removeThis);

            return Ok("Booking removed from database");

        }


    }
}
