using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<bool> IsBrandUsed(int brandId);
    }
}