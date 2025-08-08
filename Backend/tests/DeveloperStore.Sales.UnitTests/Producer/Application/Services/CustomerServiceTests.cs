using Moq;
using DeveloperStore.Sales.Application.Services.Sales;
using DeveloperStore.Sales.Application.Sales.Commands.CreateCustomer;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateCustomer;
using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Interfaces.Repositories;
using DeveloperStore.Sales.Domain.Exceptions;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Services
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task CreateCustomerAsync_Should_Create_And_Return_CustomerDto()
        {
            var command = new CreateCustomerCommand("Cliente");

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);

            var service = new CustomerService(repoMock.Object);

            var result = await service.CreateCustomerAsync(command);

            Assert.NotNull(result);
            Assert.Equal(command.CustomerName, result.CustomerName);
        }

        [Fact]
        public async Task GetAllCustomersAsync_Should_Return_All_Customers()
        {
            var list = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), CustomerName = "A" },
                new Customer { Id = Guid.NewGuid(), CustomerName = "B" }
            };

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

            var service = new CustomerService(repoMock.Object);

            var result = await service.GetAllCustomersAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCustomerByIdAsync_Should_Return_CustomerDto_When_Found()
        {
            var id = Guid.NewGuid();
            var customer = new Customer { Id = id, CustomerName = "Cliente" };

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(customer);

            var service = new CustomerService(repoMock.Object);

            var result = await service.GetCustomerByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_Should_Return_Null_When_Not_Found()
        {
            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Customer)null!);

            var service = new CustomerService(repoMock.Object);

            var result = await service.GetCustomerByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateCustomerAsync_Should_Update_And_Return_CustomerDto()
        {
            var id = Guid.NewGuid();
            var command = new UpdateCustomerCommand(id, "Novo Nome");
            var customer = new Customer { Id = id, CustomerName = "Antigo Nome" };

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(customer);
            repoMock.Setup(r => r.UpdateAsync(customer)).Returns(Task.CompletedTask);

            var service = new CustomerService(repoMock.Object);

            var result = await service.UpdateCustomerAsync(command);

            Assert.Equal(command.CustomerName, result.CustomerName);
        }

        [Fact]
        public async Task UpdateCustomerAsync_Should_Throw_When_Customer_Not_Found()
        {
            var command = new UpdateCustomerCommand(Guid.NewGuid(), "Novo Nome");

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Customer)null!);

            var service = new CustomerService(repoMock.Object);

            await Assert.ThrowsAsync<Exception>(() => service.UpdateCustomerAsync(command));
        }

        [Fact]
        public async Task DeleteCustomerAsync_Should_Delete_And_Return_CustomerDto()
        {
            var id = Guid.NewGuid();
            var customer = new Customer { Id = id, CustomerName = "Cliente" };

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(customer);
            repoMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            var service = new CustomerService(repoMock.Object);

            var result = await service.DeleteCustomerAsync(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task DeleteCustomerAsync_Should_Throw_When_Not_Found()
        {
            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Customer)null!);

            var service = new CustomerService(repoMock.Object);

            await Assert.ThrowsAsync<DeveloperStoreException>(() => service.DeleteCustomerAsync(Guid.NewGuid()));
        }
    }
}
