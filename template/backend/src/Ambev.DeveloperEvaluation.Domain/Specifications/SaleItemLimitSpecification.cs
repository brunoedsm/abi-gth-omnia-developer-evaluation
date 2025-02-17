using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    /// <summary>
    /// Spec that check SaleItems amount and SaleItems quantity individually
    /// </summary>
    public class SaleItemLimitSpecification : ISpecification<Sale>
    {
        public bool IsSatisfiedBy(Sale sale)
        {
            return (sale.Items.GroupBy(x => x.Product)
                   .Any(group => group.Sum(x => x.Quantity) > 20) ||
                   sale.Items.Any(s => s.Quantity > 20));
        }
    }
}
