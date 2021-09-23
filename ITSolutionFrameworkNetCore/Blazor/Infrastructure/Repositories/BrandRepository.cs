using ITSolution.Framework.Blazor.Application.Interfaces.Repositories;
using ITSolution.Framework.Blazor.Domain.Entities.Catalog;

namespace ITSolution.Framework.Blazor.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand, int> _repository;

        public BrandRepository(IRepositoryAsync<Brand, int> repository)
        {
            _repository = repository;
        }
    }
}