using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handler for Sale Update
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSaleHandler> _logger;
        private readonly string objectName = nameof(UpdateSaleHandler);
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<UpdateSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateSaleCommandResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            UpdateSaleCommandResult result = new();

            try
            {
                var existent = await _saleRepository.GetByIdAsync(command.Id) ?? throw new Exception("Resource Not Found");
                
                existent = _mapper.Map<Sale>(command);
                var saleItemLimitSpec = new SaleItemLimitSpecification();
                if (!saleItemLimitSpec.IsSatisfiedBy(existent))
                {
                    throw new DomainException("Cannot sell more than 20 items per product.");
                }

                var saleItemDiscountSpec = new SaleItemDiscountSpecification();
                if (!saleItemDiscountSpec.IsSatisfiedBy(existent))
                {
                    throw new DomainException("Cannot apply discount to less than 4 items.");
                }

                existent.CalculateTotalAmount();

                await _saleRepository.UpdateAsync(existent, cancellationToken);
                result.Success = true;

                NotifySaleModified(new SaleModifiedEvent(existent));

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
        private void NotifySaleModified(SaleModifiedEvent evt)
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