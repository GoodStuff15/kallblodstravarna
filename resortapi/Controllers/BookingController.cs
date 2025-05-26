using Microsoft.AspNetCore.Mvc;
using resortapi.Repositories;
using resortdtos;
using resortapi.Converters;
using resortlibrary.Models;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IRepository<Booking> _repo;
        private readonly BookingConverter _converter;
        public BookingController(IRepository<Booking> repo)
        {
            _repo = repo;
            _converter = new BookingConverter();
        }

        [HttpPost("{newBooking}", Name = "Add New Booking")]
        public async Task<ActionResult> AddBooking(BookingDto booking)
        {
            var newBooking = _converter.FromDTOtoObject(booking);

            if (booking == null)
            {
                return BadRequest("Adding booking failed (Booking does not exist)");
            }

            await _repo.CreateAsync(newBooking);

            return Ok($"Booking {newBooking.Id} added to Database successfully"); // Id wont exist?
        }


        [HttpGet(Name = "Get overview of all bookings")]
        public async Task<ActionResult<ICollection<BookingsOverviewDto>>> GetAllBookings()
        {
            var bookings = await _repo.GetAllAsync();

            if (!bookings.Any())
            {
                return NoContent();
            }

            var dtos = _converter.FromObjectCollection_ToOverviewCollection(bookings);

            return Ok(dtos);
        }

        [HttpGet(Name = "Get all bookings with details included")]
        public async Task<ActionResult<ICollection<BookingDto>>> GetAllBookingsWithGuestInfo()
        {
            var bookings = await _repo.GetAllWithIncludesAsync();

            if (!bookings.Any())
            {
                return NoContent();
            }

            var dtos = _converter.FromObjecttoDTO_Collection(bookings);

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
