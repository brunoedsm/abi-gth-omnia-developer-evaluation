using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler for Sale Creation
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly string objectName = nameof(CreateSaleHandler);

        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<CreateSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateSaleCommandResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {

            CreateSaleCommandResult result = new();

            try
            {
                var sale = _mapper.Map<Sale>(command);
                var saleItemLimitSpec = new SaleItemLimitSpecification();
                if (saleItemLimitSpec.IsSatisfiedBy(sale))
                {
                    throw new DomainException("Cannot sell more than 20 items per product.");
                }

                var saleItemDiscountSpec = new SaleItemDiscountSpecification();
                if (saleItemDiscountSpec.IsSatisfiedBy(sale))
                {
                    throw new DomainException("Cannot apply discount to less than 4 items.");
                }

                sale.CalculateTotalAmount();

                await _saleRepository.CreateAsync(sale, cancellationToken);
                result.Id = sale.Id;
                result.Success = true;

                NotifySaleCreated(new SaleCreatedEvent(sale));

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
        private void NotifySaleCreated(SaleCreatedEvent evt)
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