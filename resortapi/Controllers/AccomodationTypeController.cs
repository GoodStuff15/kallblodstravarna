using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccomodationTypeController : ControllerBase
    {
        private readonly AccomodationTypeRepo _repo;
        private readonly AccomodationTypeConverter _converter;

        public AccomodationTypeController(AccomodationTypeRepo accomodationTypeRepo, AccomodationTypeConverter accomodationTypeConverter)
        {
            _repo = accomodationTypeRepo;
            _converter = accomodationTypeConverter;
        }

        [HttpGet("Get all accomodationType")]
        public async Task<ActionResult<ICollection<AccomodationTypeDto>>> GetAllAccomodationTypes()
        {
            var accomodationTypes = await _repo.GetAllAsync();
            if (accomodationTypes == null || !accomodationTypes.Any())
            {
                return NotFound("No accomodation types found.");
            }
            var available = _converter.FromObjectCollection_ToOverviewCollection(accomodationTypes);
            return Ok(available);
        }
        [HttpGet("{id}", Name = "Get AccomodationType by Id")]
        public async Task<ActionResult<AccomodationTypeDto>> GetAccomodationTypeById(int id)
        {
            var accomodationType = await _repo.GetByIdAsync(id);
            if (accomodationType == null)
            {
                return NotFound($"Accomodation type with Id {id} can not be found");
            }
            var dto = _converter.FromObjectToDto(accomodationType);
            return Ok(dto);

        }
        [HttpPost(Name = "Add new accomodationType")]
        public async Task<ActionResult> AddNewAccomodationType([FromBody] AccomodationTypeDto newAccomodationType)
        {
            var accomodationType = _converter.FromDtoToObject(newAccomodationType);
            if (accomodationType == null)
            {
                return BadRequest("Invalid accomodation type data.");
            }
            accomodationType = await _repo.AddAsync(accomodationType);
            var newaccomodationType = _converter.FromObjectToDto(accomodationType);
            return CreatedAtRoute("Get AccomodationType by Id", new { id = newaccomodationType.Id }, newaccomodationType);

        }
        [HttpPut("{id}", Name = "Update AccomodationType by Id")]
        public async Task<ActionResult> UpdateAccomodationType(int id, [FromBody] AccomodationTypeDto updatedAccomodationType)
        {
            if (id != updatedAccomodationType.Id)
            {
                return BadRequest("Id mismatch.");
            }
            var existingAccoType = await _repo.GetByIdAsync(id);
            if (existingAccoType == null)
            {
                return NotFound($"Accomodation type with Id {id} does not exist.");
            }
            existingAccoType.Name = updatedAccomodationType.Name;
            existingAccoType.Description = updatedAccomodationType.Description;
            existingAccoType.BasePrice = updatedAccomodationType.BasePrice;

            await _repo.UpdateAsync(existingAccoType);
            return Ok($"AccomodationType {id} updated successfully");
            //return NoContent();
        }
        [HttpDelete("{id}", Name = "Delete AccomodationType by Id")]
        public async Task<ActionResult> DeleteAccomodationType(int id)
        {
            var existingAccoType = await _repo.GetByIdAsync(id);
            if (existingAccoType == null)
            {
                return NotFound($"Accomodation type with Id {id} does not exist.");
            }
            await _repo.DeleteAsync(existingAccoType);
            return Ok($"AccomodationType {id} deleted successfully");
        }
    }
}
