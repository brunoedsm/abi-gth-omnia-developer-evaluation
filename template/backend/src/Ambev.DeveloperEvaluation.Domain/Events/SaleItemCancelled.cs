using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleItemCancelledEvent : BaseEvent
    {
        public SaleItemCancelledEvent(SaleItem saleItem) : base(saleItem)
        {

        }
    }
}
