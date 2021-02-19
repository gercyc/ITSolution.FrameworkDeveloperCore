using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses.Identity
{
    //AspNetRoles
    //[Table("ITS_ROLES")]
    public class ApplicationRole : IdentityRole<int>
    {
    }
}
