using ITSolution.Framework.Blazor.Application.Specifications.Base;
using ITSolution.Framework.Blazor.Domain.Entities.Misc;

namespace ITSolution.Framework.Blazor.Application.Specifications.Misc
{
    public class DocumentTypeFilterSpecification : HeroSpecification<DocumentType>
    {
        public DocumentTypeFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}