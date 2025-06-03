using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resortapi.Converters;
using resortapi.Data;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccomodationController : ControllerBase
    {
        private readonly AccomodationRepo _repo;
        private readonly AccomodationConverter _converter;
        private readonly ResortContext _context;

        public AccomodationController(AccomodationRepo accomodationRepo, AccomodationConverter accomodationConverter, ResortContext context)
        {
            _repo = accomodationRepo;
            _converter = accomodationConverter;
            _context = context;
        }

        // [FromBody] kan/bör ändras till [FromQuery] om vi vill skicka in datumen som query-parametrar istället för i body

        [HttpGet("availableReceptionist")]
        public async Task<ActionResult<ICollection<AvailableRoomDto>>> GetAvailableAccomodations(
            [FromQuery] DateTime checkIn,
            [FromQuery] DateTime checkOut,
            [FromQuery] int noOfGuests)
        {
            var accomodations = await _repo.GetAvailableByGuestNo(checkIn, checkOut, noOfGuests);

            var available = accomodations.Select(a => new AvailableRoomDto
            {
                Id = a.Id,
                Name = a.Name,
                AccomodationType = a.AccomodationType.Name,
                Description = a.AccomodationType.Description,
                MaxOccupancy = a.MaxOccupancy,
                BasePrice = a.AccomodationType.BasePrice,
                Accessibility = a.Accessibilities?.Select(acc => new AccessibilityDto
                {
                    Name = acc.Name,
                    Description = acc.Description
                }).ToList() ?? new List<AccessibilityDto>()
            }).ToList();

            return Ok(available);
        }


        [HttpGet("availableGuest")]
        public async Task<ActionResult<ICollection<AvailableRoomDto>>> GetAvailableAccomodationsExclGuests(
            [FromQuery] DateTime checkIn,
            [FromQuery] DateTime checkOut)
        {
            var accomodations = await _repo.GetAvailableAsync(checkIn, checkOut);

            var available = accomodations.Select(a => new AvailableRoomDto
            {
                Id = a.Id,
                Name = a.Name,
                AccomodationType = a.AccomodationType.Name,
                Description = a.AccomodationType.Description,
                MaxOccupancy = a.MaxOccupancy,
                BasePrice = a.AccomodationType.BasePrice,
                Accessibility = a.Accessibilities?.Select(acc => new AccessibilityDto
                {
                    Name = acc.Name,
                    Description = acc.Description
                }).ToList() ?? new List<AccessibilityDto>()
            }).ToList();

            return Ok(available);
        }
        //[Authorize(Roles = "Staff, Admin")]
        [HttpGet("Get all Accomodations")] // all accomodations available/not available
        public async Task<ActionResult<ICollection<AvailableRoomDto>>> GetAllAccomodations()
        {
            var accomodations = await _repo.GetAllAsync();
            var available = _converter.FromObjectCollection_ToOverviewCollection(accomodations);
            return Ok(available);
        }
        [HttpGet("{id}", Name = "Get Accomodation by Id")]
        public async Task<ActionResult<AvailableRoomDto>> GetAccomodationById(int id)
        {
            var accomodation = await _repo.GetByIdAsync(id);
            if (accomodation == null)
            {
                return NotFound($"Accomodation with Id {id} can not be found");
            }
            var dto = _converter.FromObjecttoDTO(accomodation);
            return Ok(dto);

        }
        //[Authorize(Roles = "Staff, Admin")]
        [HttpPost(Name = "Add New Accomodation")]
        public async Task<ActionResult> AddNewAccomodation([FromBody] AccomodationDto newAccomodation)
        {
            var accomodation = _converter.FromDTOtoObject(newAccomodation, _context);
            if (accomodation == null)
            {
                return BadRequest("Can't add accomodation");
            }
            accomodation = await _repo.AddAsync(accomodation);
            var newAcc = _converter.FromObjecttoDTO(accomodation);
            return CreatedAtRoute("Get Accomodation by Id", new { id = newAcc.Id }, newAcc);
        }
        [HttpPut("{id}", Name = "Update Accomodation")]
        public async Task<ActionResult> UpdateAccomodation(int id, [FromBody] AccomodationDto updatedAccomodation)
        {
            var existingAccomodation = await _repo.GetByIdAsync(id);
            if (existingAccomodation == null)
            {
                return NotFound($"Accomodation with Id {id} can not be found");
            }
            existingAccomodation.Name = updatedAccomodation.Name;
            existingAccomodation.MaxOccupancy = updatedAccomodation.MaxOccupancy;
            existingAccomodation.AccomodationTypeId = updatedAccomodation.AccomodationTypeId;
            existingAccomodation.Accessibilities.Clear();
            foreach (var accessibilityId in updatedAccomodation.AccessibilityIds)
            {
                var accessibility = await _context.Accessibilities.FindAsync(accessibilityId);
                if (accessibility != null)
                {
                    existingAccomodation.Accessibilities.Add(accessibility);
                }
            }
            var save = await _repo.UpdateAsync(existingAccomodation);
            var dto = _converter.FromObjecttoDTO(save);
            return Ok(dto);
        }
        [HttpDelete("{id}", Name = "Delete Accomodation")]
        public async Task<ActionResult> DeleteAccomodation(int id)
        {
            var existingAccomodation = await _repo.GetByIdAsync(id);
            if (existingAccomodation == null)
            {
                return NotFound($"Accomodation with Id {id} can not be found");
            }
            await _repo.DeleteAsync(existingAccomodation);
            return Ok($"Accomodation {id} deleted successfully");
        }





    }
}
