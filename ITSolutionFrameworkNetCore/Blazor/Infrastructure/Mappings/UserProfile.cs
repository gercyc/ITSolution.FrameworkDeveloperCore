using AutoMapper;
using ITSolution.Framework.Blazor.Infrastructure.Models.Identity;
using ITSolution.Framework.Blazor.Application.Responses.Identity;

namespace ITSolution.Framework.Blazor.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, BlazorHeroUser>().ReverseMap();
            CreateMap<ChatUserResponse, BlazorHeroUser>().ReverseMap()
                .ForMember(dest => dest.EmailAddress, source => source.MapFrom(source => source.Email)); //Specific Mapping
        }
    }
}