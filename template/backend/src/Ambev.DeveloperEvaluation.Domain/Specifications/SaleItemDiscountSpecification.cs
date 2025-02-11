using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public class SaleItemDiscountSpecification : ISpecification<Sale>
    {
        public bool IsSatisfiedBy(Sale sale)
        {
            return (sale.Items.Any(item => item.Quantity < 4 && item.Discount > 0));
        }
    }
}
