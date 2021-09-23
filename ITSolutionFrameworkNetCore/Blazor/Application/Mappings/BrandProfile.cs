using AutoMapper;
using ITSolution.Framework.Blazor.Application.Features.Brands.Commands.AddEdit;
using ITSolution.Framework.Blazor.Application.Features.Brands.Queries.GetAll;
using ITSolution.Framework.Blazor.Application.Features.Brands.Queries.GetById;
using ITSolution.Framework.Blazor.Domain.Entities.Catalog;

namespace ITSolution.Framework.Blazor.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}