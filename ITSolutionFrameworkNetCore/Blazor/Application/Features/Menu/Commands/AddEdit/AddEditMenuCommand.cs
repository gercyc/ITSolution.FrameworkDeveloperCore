using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ITSolution.Framework.Blazor.Application.Interfaces.Repositories;
using ITSolution.Framework.Blazor.Domain.Entities.Catalog;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using ITSolution.Framework.Blazor.Shared.Constants.Application;
using ITSolution.Framework.Blazor.Domain.Entities.Menu;

namespace ITSolution.Framework.Blazor.Application.Features.Menus.Commands.AddEdit
{
    public partial class AddEditMenuCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string MenuName { get; set; }
        [Required]
        public int? ParentMenu { get; set; }
        public string MenuAction { get; set; }

    }

    internal class AddEditMenuCommandHandler : IRequestHandler<AddEditMenuCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditMenuCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditMenuCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditMenuCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditMenuCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var menu = _mapper.Map<ApplicationMenu>(command);
                await _unitOfWork.Repository<ApplicationMenu>().AddAsync(menu);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBrandsCacheKey);
                return await Result<int>.SuccessAsync(menu.Id, _localizer["Menu Saved"]);
            }
            else
            {
                var menu = await _unitOfWork.Repository<ApplicationMenu>().GetByIdAsync(command.Id);
                if (menu != null)
                {
                    menu.MenuName = command.MenuName?? menu.MenuName;
                    menu.ParentMenu = command.ParentMenu ?? menu.ParentMenu;
                    await _unitOfWork.Repository<ApplicationMenu>().UpdateAsync(menu);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBrandsCacheKey);
                    return await Result<int>.SuccessAsync(menu.Id, _localizer["Menu Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Menu Not Found!"]);
                }
            }
        }
    }
}