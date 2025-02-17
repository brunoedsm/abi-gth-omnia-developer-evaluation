using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class SaleTestData
    {
        public static Sale FeedSale(Guid id, int saleItemCount, decimal saleItemPrice, int saleQuantity)
        {

            Sale sale = new();
            sale.Id = id;
            sale.IsCancelled = false;
            sale.Branch = "Branch One";
            sale.Customer = "Customer One";
            sale.Date = DateTime.Now;

            for (var i = 0; i < saleItemCount; i++)
            {
                sale.Items.Add(new SaleItem()
                {
                    Id = Guid.NewGuid(),
                    Product = "Product One",
                    IsCancelled = false,
                    Quantity = saleQuantity,
                    UnitPrice = saleItemPrice,
                });
                sale.TotalAmount += (saleItemPrice * saleQuantity);
            }

            return sale;
        }
    }
}
