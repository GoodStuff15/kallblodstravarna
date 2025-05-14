using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using restortlibrary.Data;
using restortlibrary.Models;
using restortlibrary.Repositories;

namespace accomodationtests;

[TestClass]
public class RepositoryTests
{
   // This will be used for "live" tests
   // private readonly ResortContext _context;

    public IRepository<Customer> MockCustomerRepo;

    public RepositoryTests()
    {
        // Mock Services

        var services = new ServiceCollection();

        services.AddTransient<IRepository<Customer>, CustomerRepo>();
        services.AddDbContext<ResortContext>(options =>
                options.UseSqlServer("Data Source=DESKTOP-IGVAOCU;Database=ResortDb;Integrated Security=True;Trust Server Certificate=true;",
                b => b.MigrationsAssembly("resortapi")));

        var serviceProvider = services.BuildServiceProvider();

        MockCustomerRepo = serviceProvider.GetService<IRepository<Customer>>();

    }

    [TestMethod]
    public void GetCustomerById_FromCustomerRepo()
    {
        // Given a Customer Repository with a customer in it
        var customer = new Customer() { Id = 1, FirstName = "Gustav" };
        MockCustomerRepo.Create(customer);
        // When searching for a Customer with Id == 1
        var actual = MockCustomerRepo.Get(1);
        var expected = customer;

        // Then should return customer with id 1
        Assert.IsInstanceOfType<Customer>(actual);
        Assert.AreEqual(expected, actual);
      

    }
}
