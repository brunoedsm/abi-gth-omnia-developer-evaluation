using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Handler for Sale Item Deletion
    /// </summary>
    public class DeleteSaleItemHandler : IRequestHandler<DeleteSaleItemCommand, DeleteSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DeleteSaleItemNotificationService _notificationService;
        private readonly ILogger<DeleteSaleItemHandler> _logger;
        private readonly string objectName = nameof(DeleteSaleItemHandler);

        public DeleteSaleItemHandler(ISaleRepository saleRepository, DeleteSaleItemNotificationService notificationService, ILogger<DeleteSaleItemHandler> logger)
        {
            _saleRepository = saleRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task<DeleteSaleCommandResult> Handle(DeleteSaleItemCommand command, CancellationToken cancellationToken)
        {

            DeleteSaleCommandResult result = new();
            List<SaleItem> itemsCancelled = [];

            try
            {
                var existent = await _saleRepository.GetByIdAsync(command.SaleId) ?? throw new Exception("Resource (Sale) Not Found");

                if (!existent.Items.Any(item => item.Id == command.ItemId))
                    throw new Exception("Resource (SaleItem) Not Found");

                existent.Items.ForEach(i =>
                {
                    if (i.Id == command.ItemId)
                    {
                        i.IsCancelled = true;
                        itemsCancelled.Add(i);
                    }
                });

                await _saleRepository.UpdateAsync(existent, cancellationToken);

                foreach (var item in itemsCancelled)
                {
                    var notificationResult = _notificationService.Notify(new SaleItemCancelledEvent(item));
                    _logger.LogInformation($"[{objectName}] - {notificationResult}");
                }

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
