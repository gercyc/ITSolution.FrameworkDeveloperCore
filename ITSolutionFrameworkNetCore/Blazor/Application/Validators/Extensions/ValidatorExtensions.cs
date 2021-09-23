using ITSolution.Framework.Blazor.Application.Interfaces.Serialization.Serializers;
using FluentValidation;
using FluentValidation.Validators;

namespace ITSolution.Framework.Blazor.Application.Validators.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeJson<T>(this IRuleBuilder<T, string> ruleBuilder, IPropertyValidator<T, string> validator) where T : class
        {
            return ruleBuilder.SetValidator(validator);
        }
    }
}