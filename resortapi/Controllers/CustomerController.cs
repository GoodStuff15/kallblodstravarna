using Microsoft.AspNetCore.Mvc;
using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> _repo;
        private readonly IConverter<Customer, CreateCustomerRequestDTO> _converter;

        public CustomerController(IRepository<Customer> repo, IConverter<Customer, CreateCustomerRequestDTO> converter)
        {
            _repo = repo;
            _converter = converter;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _repo.GetAsync(id);
            if (customer == null)
                return NotFound();

            var customerDTO = _converter.FromObjecttoDTO(customer);
            return Ok(customerDTO);
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

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customerDtoResponse);
        }




    }
}
