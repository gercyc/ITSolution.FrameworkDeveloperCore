using System.Collections.Generic;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Application.Features.DocumentTypes.Commands.AddEdit;
using ITSolution.Framework.Blazor.Application.Features.DocumentTypes.Queries.GetAll;
using ITSolution.Framework.Blazor.Shared.Wrapper;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Misc.DocumentType
{
    public interface IDocumentTypeManager : IManager
    {
        Task<IResult<List<GetAllDocumentTypesResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditDocumentTypeCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}