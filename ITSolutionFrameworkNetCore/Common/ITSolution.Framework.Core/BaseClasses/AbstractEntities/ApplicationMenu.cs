using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITSolution.Framework.Core.Common.BaseClasses.AbstractEntities;

[Serializable]
[Table("ITS_MENU")]
public class ApplicationMenu: Entity<string>
{
    public string NomeMenu { get; set; }
    public string MenuPai { get; set; }
        
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