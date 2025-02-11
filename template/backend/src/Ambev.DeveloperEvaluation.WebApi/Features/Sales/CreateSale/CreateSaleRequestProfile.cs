using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Profile for mapping between Presentation and Application layer for CreateSale
    /// </summary>
    public class CreateSaleRequestProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSale
        /// </summary>
        public CreateSaleRequestProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<SaleItemRequest, CreateSaleItemCommand>();
        }
    }
}
