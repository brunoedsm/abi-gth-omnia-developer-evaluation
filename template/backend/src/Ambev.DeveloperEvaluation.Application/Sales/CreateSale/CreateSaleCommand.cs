
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for Sale Creation
    /// <see cref="Ambev.DeveloperEvaluation.Domain.Entities.Sale"/>
    /// </summary>
    public class CreateSaleCommand : IRequest<CreateSaleCommandResult>
    {
        public DateTime Date { get; set; }
        public string? Customer { get; set; }
        public string? Branch { get; set; }
        public List<CreateSaleItemCommand>? Items { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }

    /// <summary>
    /// Command Sale Item child
    /// <see cref="Ambev.DeveloperEvaluation.Domain.Entities.SaleItem"/>
    /// </summary>
    public class CreateSaleItemCommand
    {
        public string? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public bool IsCancelled { get; set; }
    }

}
