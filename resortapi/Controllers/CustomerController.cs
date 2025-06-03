using Microsoft.AspNetCore.Mvc;
using resortapi.Converters;
using resortapi.Repositories;
using resortapi.Services;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        { 
            return Ok(_service.GetCustomer(id));
        }

        [HttpPost(Name = "Add New Customer")]
        public async Task<ActionResult<CustomerDto>> AddCustomer(CreateCustomerRequestDTO newCustomer)
        {
            if (newCustomer == null)
            {
                return BadRequest("Adding customer failed, incomplete Customer info");
            }

            var customer = _converter.FromDTOtoObject(newCustomer);
            await _repo.CreateAsync(customer);  // här ska Id vara satt efter await

            var customerDtoResponse = (_converter as CustomerConverter).FromObjectToCustomerDTO(customer);

            return CreatedAtAction(nameof(GetCustomerById), new
            {
                id = customer.Id
            }, customerDtoResponse);
        }

        [HttpPost("{newCustomer}", Name = "Add New Customer Via Service")]
        public async Task<ActionResult> AddCustomerViaService(CreateCustomerRequestDTO newCustomer)
        {
            if (service.CreateCustomer(newCustomer))
            {
                return Ok($"{newCustomer.FirstName} {newCustomer.LastName} was added to Database successfully");
            }
            else
            {
                return BadRequest("Error adding new customer to database");
            }
        }

    }
}
