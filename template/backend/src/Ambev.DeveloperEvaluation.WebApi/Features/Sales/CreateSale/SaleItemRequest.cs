namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Request for Sale Creation
    /// <see cref="Ambev.DeveloperEvaluation.Domain.Entities.SaleItem"/> for documentation
    /// </summary>
    public class SaleItemRequest
    {
        public string? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
