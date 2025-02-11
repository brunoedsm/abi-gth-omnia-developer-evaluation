using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Command for Sale Item Deletion
    /// </summary>
    public class DeleteSaleItemCommand : IRequest<DeleteSaleCommandResult>
    {
        public Guid SaleId { get; }

        public Guid ItemId { get; }

        public DeleteSaleItemCommand(Guid saleId, Guid itemId)
        {
            SaleId = saleId;
            ItemId = itemId;
        }
    }
}
