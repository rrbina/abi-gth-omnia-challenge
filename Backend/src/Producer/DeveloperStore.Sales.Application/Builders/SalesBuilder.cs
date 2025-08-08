using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Application.Sales.Commands.CreateSale;
using DeveloperStore.Sales.Domain.Helpers;
using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Helpers;

namespace DeveloperStore.Sales.Application.Builders
{
    public static class SalesBuilder
    {
        private static KeyValuePair<Guid, SaleItem> GetKeyPairValueWithDiscount(Guid saleNumber, SaleItemDto item, decimal SaleTotalWithoutDiscount)
        {
            var discount = SaleDiscountCalculator.CalculateDiscount(item.Quantity);
            var multiplier = 1.0m - discount;
            var totalWithDiscount = SaleTotalWithoutDiscount * multiplier;

            var id = Guid.NewGuid();
            var saleItem = new SaleItem
            {
                Id = id,
                Quantity = item.Quantity,
                Discount = discount,
                TotalAmount = totalWithDiscount,
                SaleId = saleNumber,
                ProductId = item.ProductId,
                IsCancelled = false
            };

            return new KeyValuePair<Guid, SaleItem>(id, saleItem);
        }

        private static KeyValuePair<Guid, SaleItem> GetKeyPairValueWithoutDiscount(Guid saleNumber, SaleItemDto item, decimal SaleTotalWithoutDiscount)
        {            
            var totalWithDiscount = SaleTotalWithoutDiscount;

            var id = Guid.NewGuid();
            var saleItem = new SaleItem
            {
                Id = id,
                Quantity = item.Quantity,
                Discount = 0,
                TotalAmount = totalWithDiscount,
                SaleId = saleNumber,
                ProductId = item.ProductId,
                IsCancelled = false
            };

            return new KeyValuePair<Guid, SaleItem>(id, saleItem);
        }

        public static void SetSaleItems(Sale sale, ICollection<SaleItemDto> saleItemDtoList)
        {
            decimal saleTotalWithoutDiscount = 0.0m;
            decimal saleTotalWithDiscount = 0.0m;
            Dictionary<Guid, SaleItem> SaleItemDict = new();
            bool hasDiscount = saleItemDtoList.Count >= 4;

            foreach (var item in saleItemDtoList)
            {
                var itemTotalWithoutDiscount = item.Quantity * item.UnitPrice;
                saleTotalWithoutDiscount += itemTotalWithoutDiscount;

                var keyPairValue = hasDiscount
                    ? GetKeyPairValueWithDiscount(sale.SaleNumber, item, itemTotalWithoutDiscount)
                    : GetKeyPairValueWithoutDiscount(sale.SaleNumber, item, itemTotalWithoutDiscount);

                var saleItem = keyPairValue.Value;
                saleTotalWithDiscount += saleItem.TotalAmount;

                SaleItemDict.Add(keyPairValue.Key, saleItem);

                decimal percentalDiscount = 0;
                if (hasDiscount && itemTotalWithoutDiscount != 0 && saleItem.TotalAmount != itemTotalWithoutDiscount)
                    percentalDiscount = saleItem.TotalAmount / itemTotalWithoutDiscount;
                
                SaleItemValidator.Validate(saleItem, percentalDiscount);
                sale.Items.Add(saleItem);
            }

            sale.TotalAmount = sale.Items.Sum(i => i.TotalAmount);
            sale.TotalDiscount = saleTotalWithoutDiscount- sale.TotalAmount;
        }

        public static Sale CreateSale(CreateSaleCommand command)
        {
            var SaleId = Guid.NewGuid();
            var sale = new Sale
            {
                SaleNumber = SaleId,
                SaleDate = command.SaleDto.SaleDate,
                CustomerId = command.SaleDto.CustomerId,
                BranchName = command.SaleDto.BranchName,
                IsCancelled = false,
                Items = new List<SaleItem>()
            };
            SetSaleItems(sale, command.SaleDto.Items);
            return sale;
        }
    }
}