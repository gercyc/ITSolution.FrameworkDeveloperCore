using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Application.Extensions;
using ITSolution.Framework.Blazor.Application.Interfaces.Repositories;
using ITSolution.Framework.Blazor.Application.Interfaces.Services;
using ITSolution.Framework.Blazor.Application.Specifications.Catalog;
using ITSolution.Framework.Blazor.Domain.Entities.Catalog;
using ITSolution.Framework.Blazor.Domain.Entities.Menu;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace ITSolution.Framework.Blazor.Application.Features.Menu.Queries.Export
{
    public class ExportMenuQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportMenuQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportMenusQueryHandler : IRequestHandler<ExportMenuQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportMenusQueryHandler> _localizer;

        public ExportMenusQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportMenusQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportMenuQuery request, CancellationToken cancellationToken)
        {
            var menuFilterSpec = new MenuFilterSpecification(request.SearchString);
            var menus = await _unitOfWork.Repository<ApplicationMenu>().Entities
                .Specify(menuFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(menus, mappers: new Dictionary<string, Func<ApplicationMenu, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["MenuName"], item => item.MenuName },
                { _localizer["ParentMenu"], item => item.ParentMenu },
                { _localizer["MenuAction"], item => item.MenuAction}
            }, sheetName: _localizer["Menus"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
