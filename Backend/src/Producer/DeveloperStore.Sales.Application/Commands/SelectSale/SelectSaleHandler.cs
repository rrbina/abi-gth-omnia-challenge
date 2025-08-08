using DeveloperStore.Sales.Domain.Entities;
using MediatR;
using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Services.Sales;

namespace DeveloperStore.Sales.Application.Sales.Commands.SelectSale
{
    public class SelectSaleHandler :
        IRequestHandler<GetAllSalesCommand, IEnumerable<SaleDto>>,
        IRequestHandler<GetSaleByIdCommand, SaleDto?>
    {
        private readonly ISaleService _saleService;

        public SelectSaleHandler(ISaleService saleQueryService)
        {
            _saleService = saleQueryService;
        }

        public async Task<IEnumerable<SaleDto>> Handle(GetAllSalesCommand request, CancellationToken cancellationToken)
        {
            return await _saleService.GetAllSaleAsync();
        }

        public async Task<SaleDto?> Handle(GetSaleByIdCommand request, CancellationToken cancellationToken)
        {
            return await _saleService.GetSaleByIdAsync(request.SaleNumber);
        }
    }
}