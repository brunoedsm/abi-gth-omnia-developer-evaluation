﻿using Ambev.DeveloperEvaluation.Domain.Events;
using System.Text.Json;
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
                var existent = await _saleRepository.GetByIdAsync(command.Id) ?? throw new Exception("Resource (Sale) Not Found");
                
                existent.IsCancelled = true;

                await _saleRepository.UpdateAsync(existent, cancellationToken);
                
                NotifySaleCancelled(new SaleCancelledEvent(existent));

                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{objectName}] - Error: {ex.Message}", ex);
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Generic message broker event sent
        /// </summary>
        /// <param name="evt"></param>
        private void NotifySaleCancelled(SaleCancelledEvent evt)
        {
            var message = JsonSerializer.Serialize(new
            {
                Event = nameof(evt),
                Data = JsonSerializer.Serialize(evt.Sale),
                Timestamp = DateTime.UtcNow
            });

            // Simulação do envio ao message broker
            _logger.LogInformation("Publishing event to Message Broker: {Message}", message);
        }
    }
}
