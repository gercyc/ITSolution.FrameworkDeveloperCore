using ITSolution.Framework.Blazor.Application.Features.Menus.Commands.AddEdit;
using ITSolution.Framework.Blazor.Application.Features.Menus.Queries.GetAll;
using ITSolution.Framework.Blazor.Domain.Entities.Menu;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Menu
{
    public interface IMenuManager : IManager
    {
        Task<IResult<List<GetAllMenusResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditMenuCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}
