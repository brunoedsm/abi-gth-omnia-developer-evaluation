using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Command for Sale Retrieve
    /// </summary>
    public class GetSaleCommand : IRequest<GetSaleCommandResult>
    {
        public Guid Id { get; }

        public GetSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
