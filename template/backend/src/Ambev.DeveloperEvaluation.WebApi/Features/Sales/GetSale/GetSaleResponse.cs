
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleResponse
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string? Customer { get; set; }

        public string? Branch { get; set; }

        public List<GetSaleItemResponse>? Items { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsCancelled { get; set; }

    }

    public class GetSaleItemResponse
    {
        public Guid Id { get; set; }

        public string? Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public bool IsCancelled { get; set; }
    }
}
