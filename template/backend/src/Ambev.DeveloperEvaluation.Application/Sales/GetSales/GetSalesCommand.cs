using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    /// <summary>
    /// Command for Sales Retrieve
    /// </summary>
    public class GetSalesCommand : IRequest<GetSalesCommandResult>
    {
        public int? Skip { get; }

        public int? Take { get; }
        
        public GetSalesCommand(int? skip, int? take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
