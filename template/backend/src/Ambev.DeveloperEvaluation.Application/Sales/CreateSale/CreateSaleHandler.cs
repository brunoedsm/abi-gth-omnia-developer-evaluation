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
        private readonly CreateSaleNotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;

        private readonly string objectName = nameof(CreateSaleHandler);

        public CreateSaleHandler(ISaleRepository saleRepository,CreateSaleNotificationService notificationService, IMapper mapper, ILogger<CreateSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _notificationService = notificationService;
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

                var notificationResult = _notificationService.Notify(new SaleCreatedEvent(sale));

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