using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleItemCancelledEvent
    {
        public SaleItem SaleItem { get; }

        public SaleItemCancelledEvent(SaleItem saleItem)
        {
            SaleItem = saleItem;
        }
    }
}
