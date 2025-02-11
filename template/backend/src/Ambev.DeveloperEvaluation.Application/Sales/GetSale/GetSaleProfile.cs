using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Profile for mapping between CreateSaleCommand and Sale Domain Entity
    /// </summary>
    public class GetSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSaleCommand
        /// </summary>
        public GetSaleProfile()
        {
            CreateMap<Sale, GetSaleCommandResult>();
            CreateMap<SaleItem, GetSaleItemCommandResult>();
        }
    }
}
