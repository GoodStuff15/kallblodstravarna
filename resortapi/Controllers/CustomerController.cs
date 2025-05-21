using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restortlibrary.Converters;
using restortlibrary.Models;
using restortlibrary.Models.DTOs;
using restortlibrary.Repositories;

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

        [HttpPost("{newCustomer}", Name = "Add New Customer")]
        public async Task<ActionResult> AddCustomer(CreateCustomerRequestDTO newCustomer)
        {
            if(newCustomer == null)
            {
                return BadRequest("Adding customer failed, incomplete Customer info");
            }

            var customer = _converter.FromDTOtoObject(newCustomer);

            await _repo.CreateAsync(customer);

            return Ok($"{customer.FirstName} {customer.LastName} was added to Database successfully");
        }

    }
}
