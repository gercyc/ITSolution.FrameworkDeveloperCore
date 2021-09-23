using ITSolution.Framework.Blazor.Application.Features.Menus.Commands.AddEdit;
using ITSolution.Framework.Blazor.Application.Features.Menus.Queries.GetAll;
using ITSolution.Framework.Blazor.Client.Infrastructure.Extensions;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Menu
{
    public class MenuManager : IMenuManager
    {
        private readonly HttpClient _httpClient;

        public MenuManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.MenusEndpoints.Export
                : Routes.MenusEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();

        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.MenusEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllMenusResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.MenusEndpoints.GetAll);
            return await response.ToResult<List<GetAllMenusResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditMenuCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.MenusEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}
