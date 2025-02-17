using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    /// <summary>
    /// Command result for Sales Retrieve
    /// </summary>
    public class GetSalesCommandResult : BaseResult
    {
        public IEnumerable<GetSaleCommandResult>? Sales { get; set; }
    }
}
