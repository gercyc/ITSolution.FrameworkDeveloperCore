using ITSolution.Framework.Blazor.Domain.Entities.ExtendedAttributes;
using ITSolution.Framework.Blazor.Domain.Entities.Misc;
using Microsoft.Extensions.Localization;

namespace ITSolution.Framework.Blazor.Application.Validators.Features.ExtendedAttributes.Commands.AddEdit
{
    public class AddEditDocumentExtendedAttributeCommandValidator : AddEditExtendedAttributeCommandValidator<int, int, Document, DocumentExtendedAttribute>
    {
        public AddEditDocumentExtendedAttributeCommandValidator(IStringLocalizer<AddEditExtendedAttributeCommandValidatorLocalization> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}