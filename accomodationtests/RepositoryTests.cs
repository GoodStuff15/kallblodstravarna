using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using restortlibrary.Data;
using restortlibrary.Models;
using restortlibrary.Repositories;

namespace accomodationtests;

[TestClass]
public class RepositoryTests
{
    public IRepository<Customer> MockCustomerRepo;
    public RepositoryTests()
    {
        var services = new ServiceCollection();

        services.AddTransient<IRepository<Customer>, CustomerRepo>();
        services.AddDbContext<ResortContext>(options =>
                options.UseSqlServer("Data Source=DESKTOP-IGVAOCU;Database=ResortDb;Integrated Security=True;Trust Server Certificate=true;",
                b => b.MigrationsAssembly("resortapi")));

        var serviceProvider = services.BuildServiceProvider();

        MockCustomerRepo = serviceProvider.GetService<IRepository<Customer>>();
    }
    [TestMethod]
    public void CustomerRepo_AddNewCustomer()
    {
        // Given a newly created customer being added 
        var customer = new Customer() { Id = 1, FirstName = "Gustav" };
        MockCustomerRepo.CreateAsync(customer);
        // When checking if this customer is inserted in db
        var actual = MockCustomerRepo.GetAsync(customer.Id).Result;

        var expected = customer;
        // Then customer should be returned
        Assert.AreEqual<Customer>(expected, actual);
    }

    [TestMethod]
    public void CustomerRepo_UpdateExistingCustomer()
    {
        // Given a customer in database
        var newCustomer = new Customer() { Id = 1, FirstName = "Gustav" };
        MockCustomerRepo.CreateAsync(newCustomer);


        // When updating first name of customer and saving
        var customer = MockCustomerRepo.GetAsync(1).Result;
        customer.FirstName = "Olof";
        MockCustomerRepo.UpdateAsync(customer);

        // Then customers name should be updated.
        var actual = MockCustomerRepo.GetAsync(1).Result.FirstName;
        var expected = "Olof";

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CustomerRepo_DeleteCustomer()
    {
        // Given a customer in database
        var customer = new Customer() { Id = 1, FirstName = "Gustav" };
        MockCustomerRepo.CreateAsync(customer);

        // When trying to delete this customer
        var deleteThis = MockCustomerRepo.GetAsync(1).Result;
        MockCustomerRepo.DeleteAsync(deleteThis);

        // Then should not be found in database

        var actual = MockCustomerRepo.GetAsync(1).Result;

        Assert.IsNull(actual);

    }

    [TestMethod]
    public void CustomerRepo_GetCustomerById()
    {
        // Given a customer in database
        var customer = new Customer() { Id = 1, FirstName = "Gustav" };
        MockCustomerRepo.CreateAsync(customer);

        // When searching for customer by id
        var actual = MockCustomerRepo.GetAsync(1).Result;
        var expected = customer;

        // Then should find this customer
        Assert.AreEqual(expected, actual);
        
    }

    [TestMethod]
    public void CustomerRepo_GetAllCustomers()
    {
        // Given several customers in database
        var customer = new Customer() { Id = 1, FirstName = "Gustav" };
        var customer2 = new Customer() { Id = 2, FirstName = "Stefan" };
        var customer3 = new Customer() { Id = 3, FirstName = "Olof" };
        MockCustomerRepo.CreateAsync(customer);
        MockCustomerRepo.CreateAsync(customer2);
        MockCustomerRepo.CreateAsync(customer3);

        // When getting all customers from database
        var actual = MockCustomerRepo.GetAllAsync().Result.ToList();
        var expected = new List<Customer>();

        // Then should find this customer
        CollectionAssert.Contains(actual,customer);
        CollectionAssert.AreEqual(expected, actual);

    }
}
