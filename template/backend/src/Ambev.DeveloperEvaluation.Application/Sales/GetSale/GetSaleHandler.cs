using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetSaleCommandResult> Handle(GetSaleCommand command, CancellationToken cancellationToken)
        {

            GetSaleCommandResult result = new();

            try
            {
                

                var existentSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

                if (existentSale != null)
                {
                    result = _mapper.Map<GetSaleCommandResult>(existentSale);
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
