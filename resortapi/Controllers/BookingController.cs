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
            var booking = await _service.GetBooking(id);

            return Ok(booking);
        }

        [HttpGet("customer/{customerId}", Name = "Get all bookings belonging to customer id")]
        public async Task<ActionResult<BookingDetailsDto>> GetCustomerBookings(int customerId)
        {
            var booking = await _service.GetCustomerBookings(customerId);

            return Ok(booking);
        }

        [HttpGet("customersearch")]
        public async Task<ActionResult<ICollection<BookingsOverviewDto>>> GetCustomerBookingsByIdAndEmail(
    [FromQuery] int customerId,
    [FromQuery] string email)
        {
            // Validering
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email måste anges.");

            var bookings = await _service.GetCustomerBookingsByIdAndEmail(customerId, email);

            if (bookings == null || !bookings.Any())
                return NotFound("Inga bokningar hittades med angivna uppgifter.");

            return Ok(bookings);
        }



        //[Authorize(Roles = "Staff, Admin")]
        [HttpPost(Name = "Add New Booking")]
        public async Task<ActionResult<BookingDetailsDto>> AddBooking(BookingDto booking)
        {
            var result = await _service.CreateBooking(booking);

            return Ok(result);
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet("overview", Name = "Get bookings overview")]
        public async Task<ActionResult<ICollection<BookingsOverviewDto>>> GetBookingsOverview()
        {
            var result = await _service.GetBookingsOverview();

            return Ok(result);
        }

        [HttpGet("Detailed overview", Name = "Detailed overview")]
        public async Task<ActionResult<ICollection<BookingDetailsDto>>> GetDetailedOverview()
        {
            var result = await _service.GetDetailedOverview();

            return Ok(result);
        }


        [HttpPut("{cancelById}", Name = "Cancel booking")]

        public async Task<ActionResult> CancelBooking(int cancelById)
        {

            await _service.CancelBooking(cancelById);

            return Ok("Booking cancelled");

        }



        [HttpPut("modify/{id}", Name = "Modify booking details")]
        public async Task<ActionResult> ModifyBookingDetails(int id, [FromBody] ModifyBookingDto booking)
        {
            var modified = await _service.ModifyBooking(booking);

            return Ok($"Booking #{id} has been modified");

        }

        //[Authorize(Roles = "Staff, Admin")]
        [HttpDelete("{deleteById}", Name = "Delete booking from database")]
        public async Task<ActionResult> RemoveBookingFromDb(int deleteById)
        {
            await _service.RemoveBooking(deleteById);

            return Ok("Booking removed from db");

        }


    }
}
