using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.CreateProduct
{
    [ExcludeFromCodeCoverage]
    public class CreateProductCommand : IRequest<Guid>
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

        public CreateProductCommand() { }

        public CreateProductCommand(string productName, decimal unitPrice)
        {
            ProductName = productName;
            UnitPrice = unitPrice;
        }
    }
}