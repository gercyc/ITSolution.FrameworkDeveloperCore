using ITSolution.Framework.Blazor.Application.Responses.Audit;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Audit
{
    public interface IAuditManager : IManager
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync();

        Task<IResult<string>> DownloadFileAsync(string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
    }
}