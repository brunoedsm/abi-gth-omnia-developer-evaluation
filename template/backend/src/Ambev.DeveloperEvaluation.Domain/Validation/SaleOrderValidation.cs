using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public static class SaleOrderValidation
    {
        public static void CalculateTotalAmount(this Sale sale)
        {
            sale.TotalAmount = sale.Items.Sum(item => CalculateItemTotal(item));
        }

        private static decimal CalculateItemTotal(SaleItem item)
        {
            var discount = GetDiscountForQuantity(item.Quantity);
            return item.Quantity * item.UnitPrice * (1 - discount);
        }

        private static decimal GetDiscountForQuantity(int quantity)
        {
            return quantity switch
            {
                >= 4 and < 10 => 0.10m,
                >= 10 and <= 20 => 0.20m,
                _ => 0.0m
            };
        }
    }
}
