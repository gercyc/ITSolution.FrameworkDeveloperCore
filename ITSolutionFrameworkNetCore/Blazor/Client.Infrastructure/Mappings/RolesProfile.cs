using AutoMapper;
using ITSolution.Framework.Blazor.Application.Requests.Identity;
using ITSolution.Framework.Blazor.Application.Responses.Identity;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}