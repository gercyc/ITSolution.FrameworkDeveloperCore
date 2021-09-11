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
    public class ApplicationMenu: Entity<string>
    {
        [StringLength(300)]
        public string NomeMenu { get; set; }
        public string MenuPai { get; set; }
        public bool Status { get; set; }
        [StringLength(500)]
        public string MenuText { get; set; }
        [StringLength(50)]
        public string MenuType { get; set; }
        public string ControllerClass { get; set; }
        public string ActionController { get; set; }

        [NotMapped]
        public ApplicationMenu ParentMenu { get; set; }

        [NotMapped]
        public List<ApplicationMenu> ChildMenus { get; set; }
        public ApplicationMenu()
        {
        }
    }
}
