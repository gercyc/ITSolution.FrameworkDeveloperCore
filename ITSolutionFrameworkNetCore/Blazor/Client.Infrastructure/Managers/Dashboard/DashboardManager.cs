using ITSolution.Framework.Blazor.Client.Infrastructure.Extensions;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System.Net.Http;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Application.Features.Dashboards.Queries.GetData;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Dashboard
{
    public class DashboardManager : IDashboardManager
    {
        private readonly HttpClient _httpClient;

        public DashboardManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<DashboardDataResponse>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DashboardEndpoints.GetData);
            var data = await response.ToResult<DashboardDataResponse>();
            return data;
        }
    }
}