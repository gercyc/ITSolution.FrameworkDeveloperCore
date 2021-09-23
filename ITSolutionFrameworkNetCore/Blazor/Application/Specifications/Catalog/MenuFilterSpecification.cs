using ITSolution.Framework.Blazor.Application.Specifications.Base;
using ITSolution.Framework.Blazor.Domain.Entities.Catalog;
using ITSolution.Framework.Blazor.Domain.Entities.Menu;

namespace ITSolution.Framework.Blazor.Application.Specifications.Catalog
{
    public class MenuFilterSpecification : HeroSpecification<ApplicationMenu>
    {
        public MenuFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.MenuName.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
