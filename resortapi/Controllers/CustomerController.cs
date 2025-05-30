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
        private readonly IRepository<Customer> _repo;
        private readonly IConverter<Customer, CreateCustomerRequestDTO> _converter;

        private readonly CustomerService service;

        public CustomerController(IRepository<Customer> repo, IConverter<Customer, CreateCustomerRequestDTO> converter)
        {
            _repo = repo;
            _converter = converter;
            service = new CustomerService(repo); // **
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

        [HttpPost("{newCustomer}", Name = "Add New Customer")]
        public async Task<ActionResult> AddCustomerViaService(CreateCustomerRequestDTO newCustomer)
        {
            if(service.CreateCustomer(newCustomer))
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
