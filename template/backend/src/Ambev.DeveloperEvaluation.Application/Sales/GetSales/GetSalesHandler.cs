using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    /// <summary>
    /// Handler for Sales Retrieve
    /// </summary>
    public class GetSalesHandler : IRequestHandler<GetSalesCommand, GetSalesCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesHandler> _logger;
        private readonly string objectName = nameof(GetSalesHandler);
        public GetSalesHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<GetSalesHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
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
                else
                {
                    result.Errors.Add("Sales Not Found!");
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
