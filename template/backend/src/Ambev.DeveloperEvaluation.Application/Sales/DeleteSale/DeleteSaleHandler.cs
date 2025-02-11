using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<DeleteSaleHandler> _logger;
        private readonly string objectName = nameof(DeleteSaleHandler);

        public DeleteSaleHandler(ISaleRepository saleRepository, ILogger<DeleteSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        public async Task<DeleteSaleCommandResult> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {

            DeleteSaleCommandResult result = new();

            try
            {
                await _saleRepository.DeleteAsync(command.Id, cancellationToken);
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"[{objectName}] - Error during delete handler: {ex.Message}", ex);
                result.Errors.Add(ex.Message);
            }

            return result;
        }
    }
}
