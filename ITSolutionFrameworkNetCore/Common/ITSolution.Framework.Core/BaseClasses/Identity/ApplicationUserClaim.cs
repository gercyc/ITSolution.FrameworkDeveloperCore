using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses.Identity
{
    //AspNetUserClaims
    //[Table("ITS_USER_CLAIMS")]
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
    }
}
