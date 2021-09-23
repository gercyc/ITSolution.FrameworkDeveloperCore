using AutoMapper;
using ITSolution.Framework.Blazor.Application.Interfaces.Repositories;
using ITSolution.Framework.Blazor.Domain.Entities.Catalog;
using ITSolution.Framework.Blazor.Domain.Entities.Menu;
using ITSolution.Framework.Blazor.Shared.Constants.Application;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Application.Features.Menus.Queries.GetAll
{
    public class GetAllMenusQuery : IRequest<Result<List<GetAllMenusResponse>>>
    {
        public GetAllMenusQuery()
        {
        }
    }

    internal class GetAllMenusCachedQueryHandler : IRequestHandler<GetAllMenusQuery, Result<List<GetAllMenusResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllMenusCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllMenusResponse>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<ApplicationMenu>>> getAllMenus = () => _unitOfWork.Repository<ApplicationMenu>().GetAllAsync();
            var menuList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllMenusCacheKey, getAllMenus);
            var mappedBrands = _mapper.Map<List<GetAllMenusResponse>>(menuList);
            return await Result<List<GetAllMenusResponse>>.SuccessAsync(mappedBrands);
        }
    }
}