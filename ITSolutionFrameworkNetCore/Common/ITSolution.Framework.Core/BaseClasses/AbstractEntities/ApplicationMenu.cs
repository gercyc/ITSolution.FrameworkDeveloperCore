using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSolution.Framework.Core.BaseClasses.CommonEntities
{
    [Serializable]
    [Table("ITS_MENU")]
    public class ApplicationMenu: Entity<int>
    {
        public string NomeMenu { get; set; }
        public int? MenuPai { get; set; }
        
        [NotMapped]
        public ApplicationMenu ParentMenu { get; set; }

        [NotMapped]
        public List<ApplicationMenu> ChildMenus { get; set; }

        public bool Status { get; set; }
        public string MenuText { get; set; }
        public string MenuType { get; set; }
        public string ControllerClass { get; set; }
        public string ActionController { get; set; }
        public ApplicationMenu()
        {
        }
    }
}
