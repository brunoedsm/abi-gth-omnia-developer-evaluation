using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Command for Sale Update
    /// <see cref="Ambev.DeveloperEvaluation.Domain.Entities.Sale"/>
    /// </summary>
    public class UpdateSaleCommand : IRequest<UpdateSaleCommandResult>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Customer { get; set; }
        public string? Branch { get; set; }
        public List<UpdateSaleItemCommand>? Items { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }

    /// <summary>
    /// Command Sale Item child
    /// <see cref="Ambev.DeveloperEvaluation.Domain.Entities.SaleItem"/>
    /// </summary>
    public class UpdateSaleItemCommand
    {
        public Guid Id { get; set; }
        public string? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public bool IsCancelled { get; set; }
    }

}
