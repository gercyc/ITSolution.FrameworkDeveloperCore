using ITSolution.Framework.Blazor.Application.Responses.Audit;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Services
{
    public interface IAuditService
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId);

        Task<IResult<string>> ExportToExcelAsync(string userId, string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
    }
}