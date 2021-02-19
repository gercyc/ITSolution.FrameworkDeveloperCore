using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses.Identity
{
    //AspNetRoleClaims
    //[Table("ITS_ROLE_CLAIMS")]
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {

    }
}
