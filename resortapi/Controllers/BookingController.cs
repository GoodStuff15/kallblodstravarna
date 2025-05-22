using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restortlibrary.Models;
using restortlibrary.Repositories;

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

            return Ok(bookings);
        }

        [HttpPut("{cancelById}", Name = "Cancel booking")]
        public async Task<ActionResult> CancelBooking(int cancelById)
        {
            var cancelThis = _repo.GetAsync(cancelById).Result;
            cancelThis.Active = true;
            await _repo.UpdateAsync(cancelThis);

            return Ok($"Booking #{cancelById} has been cancelled");
        }

        [HttpDelete("{deleteById}", Name = "Delete booking from database")]
        public async Task<ActionResult> RemoveBookingFromDb(int deleteById)
        {
            await _repo.DeleteAsync(_repo.GetAsync(deleteById).Result);

            return Ok("Booking removed from database");

        }

    }
}
