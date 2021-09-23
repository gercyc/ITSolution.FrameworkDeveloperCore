using ITSolution.Framework.Blazor.Application.Features.Documents.Commands.AddEdit;
using ITSolution.Framework.Blazor.Application.Features.Documents.Queries.GetAll;
using ITSolution.Framework.Blazor.Application.Requests.Documents;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Application.Features.Documents.Queries.GetById;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}