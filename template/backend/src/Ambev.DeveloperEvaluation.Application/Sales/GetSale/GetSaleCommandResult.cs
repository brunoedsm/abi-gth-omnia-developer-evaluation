using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Command result for Sale Retrieve
    /// <see cref="Ambev.DeveloperEvaluation.Domain.Entities.Sale"/> for documentation
    /// </summary>
    public class GetSaleCommandResult : BaseResult
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string? Customer { get; set; }

        public string? Branch { get; set; }

        public List<GetSaleItemCommandResult>? Items { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsCancelled { get; set; }

    }

    /// <summary>
    /// GetSaleCommandItemResult Data
    /// <see cref="Ambev.DeveloperEvaluation.Domain.Entities.SaleItem"/> for documentation
    /// </summary>
    public class GetSaleItemCommandResult
    {
        public Guid Id { get; set; }

        public string? Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public bool IsCancelled { get; set; }
    }

}
