namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Request for Sale Creation
    /// <see cref="Ambev.DeveloperEvaluation.Domain.Entities.Sale"/> for documentation
    /// </summary>
    public class CreateSaleRequest
    {
        public DateTime Date { get; set; }
        public string? Customer { get; set; }
        public string? Branch { get; set; }
        public List<SaleItemRequest>? Items { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
