using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.SelectCustomer;
using DeveloperStore.Sales.Application.Sales.Handlers;
using DeveloperStore.Sales.Application.Services.Customers;
using Moq;
public class SelectCustomerHandlerTests
{
    [Fact]
    public async Task Handle_GetAllCustomers_Should_ReturnCustomerList()
    {
        var expectedCustomers = new List<CustomerDto>
        {
            new CustomerDto { Id = Guid.NewGuid(), CustomerName = "Cliente 1" },
            new CustomerDto { Id = Guid.NewGuid(), CustomerName = "Cliente 2" }
        };

        var customerServiceMock = new Mock<ICustomerService>();
        customerServiceMock
            .Setup(s => s.GetAllCustomersAsync())
            .ReturnsAsync(expectedCustomers);

        var handler = new SelectCustomerHandler(customerServiceMock.Object);
        var result = await handler.Handle(new GetAllCustomersCommand(), CancellationToken.None);

        Assert.Equal(expectedCustomers, result);
    }

    [Fact]
    public async Task Handle_GetCustomerById_Should_ReturnCustomer()
    {
        var customerId = Guid.NewGuid();
        var expectedCustomer = new CustomerDto { Id = customerId, CustomerName = "Cliente X" };

        var customerServiceMock = new Mock<ICustomerService>();
        customerServiceMock
            .Setup(s => s.GetCustomerByIdAsync(customerId))
            .ReturnsAsync(expectedCustomer);

        var handler = new SelectCustomerHandler(customerServiceMock.Object);
        var result = await handler.Handle(new GetCustomerByIdCommand(customerId), CancellationToken.None);

        Assert.Equal(expectedCustomer, result);
    }
}
