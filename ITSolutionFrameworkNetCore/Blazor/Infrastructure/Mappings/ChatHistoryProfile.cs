using AutoMapper;
using ITSolution.Framework.Blazor.Application.Interfaces.Chat;
using ITSolution.Framework.Blazor.Application.Models.Chat;
using ITSolution.Framework.Blazor.Infrastructure.Models.Identity;

namespace ITSolution.Framework.Blazor.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}