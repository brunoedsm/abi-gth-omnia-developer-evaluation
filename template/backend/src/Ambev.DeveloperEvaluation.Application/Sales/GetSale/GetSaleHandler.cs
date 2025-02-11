using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Handler for Sale Retrieve
    /// </summary>
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSaleHandler> _logger;
        private readonly string objectName = nameof(GetSaleHandler);

        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<GetSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
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
                _logger.LogError($"[{objectName}] - Error: {ex.Message}", ex);
                result.Errors.Add(ex.Message);
            }
            return result;

        }
    }
}
