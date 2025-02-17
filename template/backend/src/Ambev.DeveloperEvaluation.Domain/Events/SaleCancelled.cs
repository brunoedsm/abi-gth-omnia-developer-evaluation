using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelledEvent : BaseEvent
    {
        public SaleCancelledEvent(Sale sale) : base(sale)
        {

        }
    }
}
