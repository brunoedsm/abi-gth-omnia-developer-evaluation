using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Handler for Sale Deletion
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DeleteSaleNotificationService _notificationService;
        private readonly ILogger<DeleteSaleHandler> _logger;
        private readonly string objectName = nameof(DeleteSaleHandler);

        public DeleteSaleHandler(ISaleRepository saleRepository, DeleteSaleNotificationService notificationService, ILogger<DeleteSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task<DeleteSaleCommandResult> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {

            DeleteSaleCommandResult result = new();

            try
            {

                var existent = await _saleRepository.GetByIdAsync(command.Id) ?? throw new Exception("Resource (Sale) Not Found");
                existent.IsCancelled = true;

                await _saleRepository.UpdateAsync(existent, cancellationToken);

                var notificationResult = _notificationService.Notify(new SaleCancelledEvent(existent));
                _logger.LogInformation($"[{objectName}] - {notificationResult}");

                result.Success = true;
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
