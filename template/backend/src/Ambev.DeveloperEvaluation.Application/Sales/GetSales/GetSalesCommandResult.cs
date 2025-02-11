using Ambev.DeveloperEvaluation.Application.Base;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// GetSaleCommandResult Data
    /// <see cref="Ambev.DeveloperEvaluation.Domain.Entities.Sale"/> for documentation
    /// </summary>
    public class GetSalesCommandResult : BaseResult
    {
        public IEnumerable<GetSaleCommandResult>? Sales { get; set; }
    }
}
