using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Profile for mapping between Command and Domain Entity
    /// </summary>
    public class GetSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for Get Sale
        /// </summary>
        public GetSaleProfile()
        {
            CreateMap<Sale, GetSaleCommandResult>();
            CreateMap<SaleItem, GetSaleItemCommandResult>();
        }
    }
}
