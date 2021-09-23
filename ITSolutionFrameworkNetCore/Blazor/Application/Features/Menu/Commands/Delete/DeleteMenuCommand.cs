using ITSolution.Framework.Blazor.Application.Interfaces.Repositories;
using ITSolution.Framework.Blazor.Domain.Entities.Catalog;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using ITSolution.Framework.Blazor.Shared.Constants.Application;

namespace ITSolution.Framework.Blazor.Application.Features.Menu.Commands.Delete
{
    public class DeleteMenuCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteBrandCommandHandler : IRequestHandler<DeleteMenuCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStringLocalizer<DeleteBrandCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteBrandCommandHandler(IUnitOfWork<int> unitOfWork, IProductRepository productRepository, IStringLocalizer<DeleteBrandCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteMenuCommand command, CancellationToken cancellationToken)
        {
            var isBrandUsed = false; //await _productRepository.IsBrandUsed(command.Id);
            if (!isBrandUsed)
            {
                var brand = await _unitOfWork.Repository<Brand>().GetByIdAsync(command.Id);
                if (brand != null)
                {
                    await _unitOfWork.Repository<Brand>().DeleteAsync(brand);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBrandsCacheKey);
                    return await Result<int>.SuccessAsync(brand.Id, _localizer["Menu Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Menu Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}