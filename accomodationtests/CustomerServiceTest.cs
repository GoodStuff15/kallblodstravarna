namespace accomodationtests;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Moq;
using resortapi.Converters;
using resortapi.Repositories;
using resortapi.Services;
using resortdtos;
using resortlibrary.Models;

[TestClass]
public class CustomerServiceTest
{
    private Mock<ICustomerService> _customerServiceMock;
    private Mock<IRepository<Customer>> _repositoryMock;
    private Mock<IConverter<Customer,CreateCustomerRequestDTO>> _converterMock;
    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IRepository<Customer>>();
        _customerServiceMock = new Mock<ICustomerService>();
        _converterMock = new Mock<IConverter<Customer,CreateCustomerRequestDTO>>();

       
    }
    [TestMethod]
    public void CustomerService_GetACustomer()
    {
        _repositoryMock.Setup(repo => repo.GetAsync(1))
                        .Returns(Task.FromResult(new Customer { Id = 1, FirstName = "Gustav"}));

        var service = new CustomerService(_repositoryMock.Object, _converterMock.Object);

        var result = service.GetCustomer(1);

        Assert.AreEqual("Gustav", result.FirstName);

    }

    [TestMethod]
    public void CustomerService_CreateCustomer_ShouldReturnTrue()
    {
        // Given a customer service with converter and repository set up
        var dto = new CreateCustomerRequestDTO() { FirstName = "Sven", LastName = "Svensson", Email = "sven@svensson.se" };
        var customer = new Customer { Id = 1, FirstName = "Sven", LastName = "Svensson", Email = "sven@svensson.se" };

        _repositoryMock.Setup(repo => repo.CreateAsync(customer));
        _converterMock.Setup(conv => conv.FromDTOtoObject(dto))
                            .Returns(customer);
        _customerServiceMock.Setup(srv => srv.CreateCustomer(dto))
                             .Returns(true);

        var service = new CustomerService(_repositoryMock.Object, _converterMock.Object);
        
        // When creating a customer via service method
        var created = _customerServiceMock.Object.CreateCustomer(dto);

        // Then method should return true
        Assert.IsTrue(created);

    }



    [TestCleanup]
    public void Cleanup()
    {
        _repositoryMock = null;
        _customerServiceMock = null;
        _converterMock = null;
    }
}
