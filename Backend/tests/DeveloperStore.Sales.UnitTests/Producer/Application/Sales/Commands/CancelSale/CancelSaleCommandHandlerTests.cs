using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.CancelSale;
using DeveloperStore.Sales.Application.Services.Sales;
using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using Moq;

public class CancelSaleCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_CallCancelSaleAsync_AndPublishEvent_AndReturnEventId()
    {
        var saleId = Guid.NewGuid();

        var sale = new Sale
        {
            SaleNumber = saleId,
            SaleDate = DateTime.UtcNow,
            Customer = new Customer(),
            Items = new List<SaleItem>()
        };

        var dto = new SaleDto(sale);

        var saleServiceMock = new Mock<ISaleService>();
        saleServiceMock
            .Setup(s => s.CancelSaleAsync(saleId))
            .ReturnsAsync(dto);

        var eventPublisherMock = new Mock<IEventPublisher>();
        eventPublisherMock
            .Setup(p => p.PublishAsync(It.IsAny<SaleCancelledEvent>()))
            .Returns(Task.CompletedTask);

        var handler = new CancelSaleCommandHandler(saleServiceMock.Object, eventPublisherMock.Object);
        var result = await handler.Handle(new CancelSaleCommand(saleId), CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result);

        eventPublisherMock.Verify(p => p.PublishAsync(It.Is<SaleCancelledEvent>(e =>
            e.SaleNumber == saleId &&
            e.EventId == result
        )), Times.Once);
    }
}