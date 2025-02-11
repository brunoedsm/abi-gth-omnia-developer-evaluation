using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSalesHandler : IRequestHandler<GetSalesCommand, GetSalesCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSalesHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetSalesCommandResult> Handle(GetSalesCommand command, CancellationToken cancellationToken)
        {

            GetSalesCommandResult result = new();

            try
            {
                var pagedSales = await _saleRepository.GetAllAsync(command.Skip, command.Take, cancellationToken);

                if (pagedSales != null)
                {
                    result.Sales = _mapper.Map<IEnumerable<GetSaleCommandResult>>(pagedSales);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;
        }
    }
}
