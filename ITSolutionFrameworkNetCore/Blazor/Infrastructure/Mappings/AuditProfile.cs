using AutoMapper;
using ITSolution.Framework.Blazor.Infrastructure.Models.Audit;
using ITSolution.Framework.Blazor.Application.Responses.Audit;

namespace ITSolution.Framework.Blazor.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}