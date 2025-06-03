using Microsoft.AspNetCore.Mvc;
using resortapi.Repositories;
using resortdtos;
using resortapi.Converters;
using resortlibrary.Models;
using Microsoft.AspNetCore.Authorization;

using resortapi.Services;


namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IBookingService _service;
        public BookingController(IBookingService service)
        { 
            _service = service;
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
        public async Task<ActionResult<BookingDetailsDto>> AddBooking(BookingDto booking)
        {

            var result = await _service.CreateBooking(booking);

            
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("overview", Name = "Get bookings overview")]
        public async Task<ActionResult<ICollection<BookingsOverviewDto>>> GetBookingsOverview()
        {
            var result = await _service.GetBookingsOverview();

            return Ok(result);
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
            cancelThis.CancellationDate = DateTime.Now;
            await _repo.UpdateAsync(cancelThis);

            return Ok($"Booking #{cancelById} has been cancelled");
        }

        [HttpPut("modify/{id}", Name = "Modify booking details")]
        public async Task<ActionResult> ModifyBookingDetails(int id, [FromBody] ModifyBookingDto booking)
        {
            var modified = await _service.ModifyBooking(booking);

            return Ok($"Booking #{id} has been modified");

            //var removeAoption = existingBooking.AdditionalOptions
            //    .Where(o => !newAdditionalOptionIds.Contains(o.Id))
            //    .ToList();
            //foreach (var option in removeAoption)
            //{
            //    existingBooking.AdditionalOptions.Remove(option);
            //}

            //foreach (var optionId in newAdditionalOptionIds)
            //{
            //    if (!existingBooking.AdditionalOptions.Any(o => o.Id == optionId))
            //    {
            //        var diffOption = await _additionalOptionRepo.GetAsync(optionId);
            //        if (diffOption == null)
            //        {
            //            return BadRequest($"Additional option with Id {optionId} does not exist");
            //        }
            //        existingBooking.AdditionalOptions.Add(diffOption);
            //    }
            //}
            //existingBooking.Guests.Clear();

            //var newGuest = booking.Guests
            //    .GroupBy(g => new { g.FirstName, g.LastName, g.Age })
            //    .Select(g => g.First())
            //    .ToList();

            //foreach (var g in newGuest)
            //{
            //    var guestExist = await _context.Guests
            //         .FirstOrDefaultAsync(x => x.FirstName == g.FirstName && x.LastName == g.LastName && x.Age == g.Age);
            //    if (guestExist != null)
            //    {
            //        existingBooking.Guests.Add(guestExist);
            //    }
            //    else
            //    {
            //        existingBooking.Guests.Add(new Guest
            //        {
            //            FirstName = g.FirstName,
            //            LastName = g.LastName,
            //            Age = g.Age
            //        });
            //    }
            //}

            //existingBooking.CheckIn = booking.CheckIn;
            //existingBooking.CheckOut = booking.CheckOut;
            //existingBooking.AccomodationId = booking.AccomodationId;

            //await _repo.UpdateAsync(existingBooking);


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
