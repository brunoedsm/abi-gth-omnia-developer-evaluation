using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<CreateSaleCommandResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            CreateSaleCommandResult result = new();

            try
            {
                //var validator = new CreateSaleCommandValidator();
                //var validationResult = await validator.ValidateAsync(command, cancellationToken);

                //if (!validationResult.IsValid)
                //    throw new ValidationException(validationResult.Errors);

                var sale = _mapper.Map<Sale>(command);
                await _saleRepository.CreateAsync(sale, cancellationToken);
                result.Id = sale.Id;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;

        }
    }
}