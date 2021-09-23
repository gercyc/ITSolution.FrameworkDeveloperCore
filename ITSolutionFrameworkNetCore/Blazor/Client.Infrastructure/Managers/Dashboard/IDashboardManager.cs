using ITSolution.Framework.Blazor.Shared.Wrapper;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Application.Features.Dashboards.Queries.GetData;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}