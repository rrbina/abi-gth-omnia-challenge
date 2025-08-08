using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.SelectSale;
using DeveloperStore.Sales.Application.Services.Sales;
using DeveloperStore.Sales.Domain.Entities;
using Moq;
public class SelectSaleHandlerTests
{
    [Fact]
    public async Task Handle_GetAllSales_Should_ReturnSaleList()
    {
        var sales = new List<Sale>
        {
            new Sale
            {
                SaleNumber = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                Customer = new Customer(),
                Items = new List<SaleItem>()
            }
        };

        var expectedSales = sales.Select(s => new SaleDto(s)).ToList();

        var saleServiceMock = new Mock<ISaleService>();
        saleServiceMock
        .Setup(s => s.GetAllSaleAsync())
        .ReturnsAsync(expectedSales);


        var handler = new SelectSaleHandler(saleServiceMock.Object);
        var result = await handler.Handle(new GetAllSalesCommand(), CancellationToken.None);

        Assert.Equal(expectedSales, result);
    }

    [Fact]
    public async Task Handle_GetSaleById_Should_ReturnSale()
    {
        var saleId = Guid.NewGuid();
        var sale = new Sale
        {
            SaleNumber = saleId,
            SaleDate = DateTime.UtcNow,
            Customer = new Customer(),
            Items = new List<SaleItem>()
        };
        var expectedSale = new SaleDto(sale);

        var saleServiceMock = new Mock<ISaleService>();
        saleServiceMock
            .Setup(s => s.GetSaleByIdAsync(saleId))
            .ReturnsAsync(expectedSale);

        var handler = new SelectSaleHandler(saleServiceMock.Object);
        var result = await handler.Handle(new GetSaleByIdCommand(saleId), CancellationToken.None);

        Assert.Equal(expectedSale, result);
    }
}