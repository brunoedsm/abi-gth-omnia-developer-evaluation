using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Profile for mapping between Presentation and Application layer for GetSale
    /// </summary>
    public class GetSaleRequestProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSale
        /// </summary>
        public GetSaleRequestProfile()
        {
            CreateMap<GetSaleCommandResult, GetSaleResponse>();
            CreateMap<GetSaleItemCommandResult, GetSaleItemResponse>();
        }
    }
}
