using Moq;
using DeveloperStore.Sales.Application.Sales.Commands.CreateSale;
using DeveloperStore.Sales.Application.Services.Sales;
using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using DeveloperStore.Sales.Application.Common;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.CreateSale
{
    public class CreateSaleCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_PublishEvent_And_ReturnEventId()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                SaleDto = new SaleDto
                {
                    SaleDate = DateTime.UtcNow,
                    CustomerId = Guid.NewGuid(),
                    CustomerName = "Cliente",
                    BranchName = "Filial",
                    Items = new List<SaleItemDto>
                    {
                        new SaleItemDto 
                        {
                            ProductId = Guid.NewGuid(),
                            UnitPrice = 10,
                            Quantity = 1
                        }
                    }
                }
            };

            var saleDtoReturned = new SaleDto
            {
                SaleNumber = Guid.NewGuid(),
                SaleDate = command.SaleDto.SaleDate,
                CustomerName = command.SaleDto.CustomerName,
                TotalAmount = 10,
                Discount = 0
            };

            var saleServiceMock = new Mock<ISaleService>();
            saleServiceMock
                .Setup(s => s.CreateSaleAsync(It.IsAny<CreateSaleCommand>()))
                .ReturnsAsync(saleDtoReturned);

            var eventPublisherMock = new Mock<IEventPublisher>();

            SaleCreatedEvent? capturedEvent = null;
            eventPublisherMock
                .Setup(e => e.PublishAsync(It.IsAny<SaleCreatedEvent>()))
                .Callback<SaleCreatedEvent>(e => capturedEvent = e)
                .Returns(Task.CompletedTask);

            var handler = new CreateSaleCommandHandler(eventPublisherMock.Object, saleServiceMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);
            Assert.NotNull(capturedEvent);
            Assert.Equal(result, capturedEvent!.EventId);
        }
    }
}
