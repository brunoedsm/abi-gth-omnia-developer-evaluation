using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Command for Sale Deletion
    /// </summary>
    public class DeleteSaleCommand : IRequest<DeleteSaleCommandResult>
    {
        public Guid Id { get; }

        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
