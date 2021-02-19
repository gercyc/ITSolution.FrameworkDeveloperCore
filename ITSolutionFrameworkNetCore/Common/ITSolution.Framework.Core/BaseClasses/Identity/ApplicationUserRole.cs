using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses.Identity
{
    //AspNetUserRoles
    //[Table("ITS_USER_ROLES")]
    public class ApplicationUserRole : IdentityUserRole<int>
    {
    }
}
