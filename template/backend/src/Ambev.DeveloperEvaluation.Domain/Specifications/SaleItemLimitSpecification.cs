using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public class SaleItemLimitSpecification : ISpecification<Sale>
    {
        public bool IsSatisfiedBy(Sale sale)
        {
            return (sale.Items.GroupBy(x => x.Product)
                   .Any(group => group.Sum(x => x.Quantity) > 20));
        }
    }
}
