using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.UpdateProduct
{
    [ExcludeFromCodeCoverage]
    public class UpdateProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

        public UpdateProductCommand() { }

        public UpdateProductCommand(Guid id, string productName, decimal unitPrice)
        {
            Id = id;
            ProductName = productName;
            UnitPrice = unitPrice;
        }
    }
}