using AutoMapper;
using ITSolution.Framework.Blazor.Application.Interfaces.Repositories;
using ITSolution.Framework.Blazor.Domain.Entities.Catalog;
using ITSolution.Framework.Blazor.Domain.Entities.Menu;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Application.Features.Menu.Queries.GetById
{
    public class GetMenuByIdQuery : IRequest<Result<GetMenuByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, Result<GetMenuByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetMenuByIdResponse>> Handle(GetMenuByIdQuery query, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Repository<ApplicationMenu>().GetByIdAsync(query.Id);
            var mappedBrand = _mapper.Map<GetMenuByIdResponse>(brand);
            return await Result<GetMenuByIdResponse>.SuccessAsync(mappedBrand);
        }
    }
}