using ITSolution.Framework.Blazor.Domain.Contracts;

namespace ITSolution.Framework.Blazor.Domain.Entities.Misc
{
    public class DocumentType : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}