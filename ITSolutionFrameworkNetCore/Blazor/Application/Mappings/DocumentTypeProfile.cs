using AutoMapper;
using ITSolution.Framework.Blazor.Application.Features.DocumentTypes.Commands.AddEdit;
using ITSolution.Framework.Blazor.Application.Features.DocumentTypes.Queries.GetAll;
using ITSolution.Framework.Blazor.Application.Features.DocumentTypes.Queries.GetById;
using ITSolution.Framework.Blazor.Domain.Entities.Misc;

namespace ITSolution.Framework.Blazor.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}