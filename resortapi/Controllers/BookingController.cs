using Microsoft.AspNetCore.Mvc;
using resortapi.Repositories;
using resortdtos;
using resortapi.Converters;
using resortlibrary.Models;
using Microsoft.AspNetCore.Authorization;
using resortapi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IRepository<Booking> _repo;
        private readonly BookingConverter _converter;
        private readonly IRepository<AdditionalOption> _additionalOptionRepo;
        private readonly ResortContext _context;
        public BookingController(IRepository<Booking> repo, IRepository<AdditionalOption> additionalOptionRepo, ResortContext context)
        {
            _repo = repo;
            _converter = new BookingConverter();
            _additionalOptionRepo = additionalOptionRepo;
            _context = context;
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

        [HttpPut("cancel/{cancelById}", Name = "Cancel booking")]
        public async Task<ActionResult> CancelBooking(int cancelById)
        {
            var cancelThis = _repo.GetAsync(cancelById).Result;

            if(cancelThis == null)
            {
                return BadRequest($"Booking with {cancelById} cannot be found!");
            }
            cancelThis.Cancelled = true;
            cancelThis.Active = false;
            cancelThis.CancellationDate = DateTime.Now;
            await _repo.UpdateAsync(cancelThis);

            return Ok($"Booking #{cancelById} has been cancelled");
        }

        [HttpPut("modify/{id}", Name = "Modify booking details")]
        public async Task<ActionResult> ModifyBookingDetails(int id, [FromBody] ModifyBookingDto booking)
        {
            if (booking.BookingId != 0 && booking.BookingId != id)
            {
                return BadRequest("Booking details are not valid or do not match the booking ID");
            }

            var existingBooking = await _repo.GetAsync(id);
            if (existingBooking == null)
            {
                return NotFound($"Booking with Id {id} does not exist");
            }
            await _context.Entry(existingBooking).Collection(b => b.Guests).LoadAsync();
            await _context.Entry(existingBooking).Collection(b => b.AdditionalOptions).LoadAsync();

            var newAdditionalOptionIds = booking.AdditionalOptionIds?.Distinct().ToList() ?? new List<int>();

            var removeAoption = existingBooking.AdditionalOptions
                .Where(o => !newAdditionalOptionIds.Contains(o.Id))
                .ToList();
            foreach (var option in removeAoption)
            {
                existingBooking.AdditionalOptions.Remove(option);
            }
            
            foreach (var optionId in newAdditionalOptionIds)
            {
                if (!existingBooking.AdditionalOptions.Any(o => o.Id == optionId))
                {
                    var diffOption = await _additionalOptionRepo.GetAsync(optionId);
                    if (diffOption == null)
                    {
                        return BadRequest($"Additional option with Id {optionId} does not exist");
                    }
                    existingBooking.AdditionalOptions.Add(diffOption);
                }
            }
            existingBooking.Guests.Clear();

            var newGuest = booking.Guests
                .GroupBy(g => new { g.FirstName, g.LastName, g.Age })
                .Select(g => g.First())
                .ToList();

            foreach (var g in newGuest)
            {
                var guestExist = await _context.Guests
                     .FirstOrDefaultAsync(x => x.FirstName == g.FirstName && x.LastName == g.LastName && x.Age == g.Age);
                if (guestExist != null)
                {
                    existingBooking.Guests.Add(guestExist);
                }
                else
                {
                    existingBooking.Guests.Add(new Guest
                    {
                        FirstName = g.FirstName,
                        LastName = g.LastName,
                        Age = g.Age
                    });
                }
            }
            
            existingBooking.CheckIn = booking.CheckIn;
            existingBooking.CheckOut = booking.CheckOut;
            existingBooking.AccomodationId = booking.AccomodationId;

            await _repo.UpdateAsync(existingBooking);
            return Ok($"Booking #{id} has been modified");

        }

        //[Authorize(Roles = "Staff, Admin")]
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
