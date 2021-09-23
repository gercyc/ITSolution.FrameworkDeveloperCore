using ITSolution.Framework.Blazor.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Domain.Entities.Menu
{
    public class ApplicationMenu : AuditableEntity<int>
    {
        public string MenuName { get; set; }
        public int? ParentMenu { get; set; }
        public string MenuAction { get; set; }

    }
}
