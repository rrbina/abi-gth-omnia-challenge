using DeveloperStore.Sales.Application.Builders;
using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.CreateSale;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateSale;
using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;
using DeveloperStore.Sales.Domain.Interfaces.Repositories;

namespace DeveloperStore.Sales.Application.Services.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public SaleService(ISaleRepository saleRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<SaleDto> UpdateSaleAsync(UpdateSaleCommand command)
        {
            var existingSale = await _saleRepository.GetByIdAsync(command.SaleNumber);
            if (existingSale == null)
                throw new DeveloperStoreException("Venda não encontrada.");

            existingSale.SaleNumber = command.SaleNumber;
            existingSale.SaleDate = command.SaleDate;
            existingSale.CustomerId = command.CustomerId;
            existingSale.Customer.CustomerName = command.CustomerName;
            existingSale.BranchName = command.BranchName;
            existingSale.Items.Clear();

            SalesBuilder.SetSaleItems(existingSale, command.Items);

            await _saleRepository.UpdateAsync(existingSale);
            return new SaleDto(existingSale);
        }

        public async Task<SaleDto> DeleteSaleAsync(Guid SaleId)
        {
            var sale = await _saleRepository.GetByIdAsync(SaleId);

            if (sale == null)
                throw new DeveloperStoreException("Venda não encontrada.");

            await _saleRepository.DeleteAsync(SaleId);
            return new SaleDto(sale);
        }

        public async Task<CancelSaleItemDto> CancelItemAsync(Guid SaleId, Guid ItemId)
        {
            var sale = await _saleRepository.GetByIdAsync(SaleId);

            if (sale == null)
                throw new DeveloperStoreException("Venda não encontrada.");

            var item = sale.Items.FirstOrDefault(i => i.Id == ItemId);
            if (item == null)
                throw new DeveloperStoreException("Item não encontrado.");

            item.IsCancelled = true;

            sale.TotalAmount = sale.Items
                .Where(i => !i.IsCancelled)
                .Sum(i => i.TotalAmount);
            await _saleRepository.UpdateAsync(sale);

            return new CancelSaleItemDto(new SaleDto(sale), new SaleItemDto(item));
        }

        public async Task<SaleDto> CancelSaleAsync(Guid SaleId)
        {
            var sale = await _saleRepository.GetByIdAsync(SaleId);

            if (sale == null)
                throw new DeveloperStoreException("Venda não encontrada.");

            sale.IsCancelled = true;

            foreach (var item in sale.Items)
                item.IsCancelled = true;

            sale.TotalAmount = 0;

            await _saleRepository.UpdateAsync(sale);

            return new SaleDto(sale);
        }

        public async Task<SaleDto> CreateSaleAsync(CreateSaleCommand command)
        {
            var sale = SalesBuilder.CreateSale(command);
            var customer = await _customerRepository.GetByIdAsync(sale.CustomerId);

            sale.Customer = customer ?? new Customer();
            foreach (var item in sale.Items)
            {
                if (item != null)
                    item.Product = await _productRepository.GetByIdAsync(item.ProductId) ??  new Product();
            }
            
            await _saleRepository.AddAsync(sale);

            return new SaleDto(sale);
        }

        public async Task<SaleDto?> GetSaleByIdAsync(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            return sale == null ? null : new SaleDto(sale);
        }

        async Task<IEnumerable<SaleDto>> ISaleService.GetAllSaleAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            IEnumerable<SaleDto> saleDtos = sales.Select(sale => new SaleDto(sale));

            return saleDtos;
        }
    }
}