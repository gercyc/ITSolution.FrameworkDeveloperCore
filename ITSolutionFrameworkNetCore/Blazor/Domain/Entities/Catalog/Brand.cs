using ITSolution.Framework.Blazor.Domain.Contracts;

namespace ITSolution.Framework.Blazor.Domain.Entities.Catalog
{
    public class Brand : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
    }
}