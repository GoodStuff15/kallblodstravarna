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
        private readonly BookingConverter _converter;
        private readonly IRepository<AdditionalOption> _additionalOptionRepo;
        public BookingController(IRepository<Booking> repo, IRepository<AdditionalOption> additionalOptionRepo)
        {
            _repo = repo;
            _converter = new BookingConverter();
            _additionalOptionRepo = additionalOptionRepo;
        }

        //[Authorize(Roles = "Staff, Admin")]
        [HttpPost(Name = "Add New Booking")]
        public async Task<ActionResult> AddBooking(BookingDto booking)
        {
            var getCustomer = await _repo.GetAsync(booking.CustomerId);
            if (getCustomer == null)
            {
                return BadRequest($"Customer with Id {booking.CustomerId} does not exist");
            }

            var additionalOptions = new List<AdditionalOption>();
            if (booking.AdditionalOptionIds != null && booking.AdditionalOptionIds.Any())
            {
                foreach (var optionId in booking.AdditionalOptionIds)
                {
                    var additionalOption = await _additionalOptionRepo.GetAsync(optionId);
                    if (additionalOption == null)
                    {
                        return BadRequest($"Additional option with Id {optionId} does not exist");
                    }
                    additionalOptions.Add(additionalOption);
                }
            }
            var newBooking = _converter.FromDTOtoObject(booking, additionalOptions);

            if (booking == null)
            {
                return BadRequest("Adding booking failed (Booking does not exist)");
            }

            newBooking.TimeOfBooking = DateTime.Now;
            await _repo.CreateAsync(newBooking);

            return Ok($"Booking added to Database successfully");
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("detailed", Name = "Get all bookings with details included")]
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
