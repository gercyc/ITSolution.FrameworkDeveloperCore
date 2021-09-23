using AutoMapper;
using ITSolution.Framework.Blazor.Application.Features.Products.Commands.AddEdit;
using ITSolution.Framework.Blazor.Domain.Entities.Catalog;

namespace ITSolution.Framework.Blazor.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}