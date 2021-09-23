using ITSolution.Framework.Blazor.Application.Features.Products.Commands.AddEdit;
using ITSolution.Framework.Blazor.Application.Features.Products.Queries.GetAllPaged;
using ITSolution.Framework.Blazor.Application.Requests.Catalog;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}