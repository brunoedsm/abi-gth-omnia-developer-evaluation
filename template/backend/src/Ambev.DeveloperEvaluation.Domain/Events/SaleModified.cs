using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent : BaseEvent
    {
        public SaleModifiedEvent(Sale sale) : base(sale)
        {

        }
    }
}
