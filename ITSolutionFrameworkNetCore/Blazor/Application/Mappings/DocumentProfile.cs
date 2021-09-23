using AutoMapper;
using ITSolution.Framework.Blazor.Application.Features.Documents.Commands.AddEdit;
using ITSolution.Framework.Blazor.Application.Features.Documents.Queries.GetById;
using ITSolution.Framework.Blazor.Domain.Entities.Misc;

namespace ITSolution.Framework.Blazor.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}