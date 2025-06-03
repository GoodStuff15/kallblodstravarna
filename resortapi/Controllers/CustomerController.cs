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
            return Ok(await _service.GetCustomer(id));
        }

        [HttpPost(Name = "Add New Customer")]
        public async Task<ActionResult<CustomerDto>> AddCustomer(CreateCustomerRequestDTO newCustomer)
        {
            var customer = await _service.CreateCustomer(newCustomer);

            return Ok(customer);

        }

    }
}
