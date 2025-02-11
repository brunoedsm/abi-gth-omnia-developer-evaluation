using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleCommandResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            UpdateSaleCommandResult result = new();

            try
            {
                var existent = await _saleRepository.GetByIdAsync(command.Id);

                if (existent == null)
                {
                    throw new Exception("Resource Not Found");
                }

                existent = _mapper.Map<Sale>(command);
                await _saleRepository.UpdateAsync(existent, cancellationToken);
                result.Id = existent.Id;
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