using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Profile for mapping between Presentation and Application layer for CreateSale
    /// </summary>
    public class UpdateSaleRequestProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSale
        /// </summary>
        public UpdateSaleRequestProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<SaleItemRequest, UpdateSaleItemCommand>();
        }
    }
}
