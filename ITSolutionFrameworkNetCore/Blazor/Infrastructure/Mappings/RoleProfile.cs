using AutoMapper;
using ITSolution.Framework.Blazor.Infrastructure.Models.Identity;
using ITSolution.Framework.Blazor.Application.Responses.Identity;

namespace ITSolution.Framework.Blazor.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}