using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resortlibrary.Models;
using resortapi.Repositories;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IRepository<Booking> _repo;
        public BookingController(IRepository<Booking> repo)
        {
            _repo = repo;
        }

        [HttpPost("{newBooking}", Name = "Add New Booking")]
        public async Task<ActionResult> AddBooking(Booking booking)
        {
            if (booking == null)
            {
                return BadRequest("Adding booking failed (Booking does not exist)");
            }

            await _repo.CreateAsync(booking);

            return Ok($"Booking {booking.Id} added to Database successfully");
        }


        [HttpGet(Name = "Get all bookings")]
        public async Task<ActionResult<ICollection<Booking>>> GetAllBookings()
        {
            var bookings = await _repo.GetAllAsync();

            if(!bookings.Any())
            {
                return NoContent();
            }

            return Ok(bookings);
        }

        [HttpPut("{cancelById}", Name = "Cancel booking")]
        public async Task<ActionResult> CancelBooking(int cancelById)
        {
            var cancelThis = _repo.GetAsync(cancelById).Result;

            if(cancelThis == null)
            {
                return BadRequest($"Booking with {cancelById} cannot be found!");
            }
            cancelThis.Active = true;
            await _repo.UpdateAsync(cancelThis);

            return Ok($"Booking #{cancelById} has been cancelled");
        }

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
