using ITSolution.Framework.Blazor.Application.Features.Brands.Commands.AddEdit;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ITSolution.Framework.Blazor.Application.Validators.Features.Brands.Commands.AddEdit
{
    public class AddEditBrandCommandValidator : AbstractValidator<AddEditBrandCommand>
    {
        public AddEditBrandCommandValidator(IStringLocalizer<AddEditBrandCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.Description)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Description is required!"]);
            RuleFor(request => request.Tax)
                .GreaterThan(0).WithMessage(x => localizer["Tax must be greater than 0"]);
        }
    }
}