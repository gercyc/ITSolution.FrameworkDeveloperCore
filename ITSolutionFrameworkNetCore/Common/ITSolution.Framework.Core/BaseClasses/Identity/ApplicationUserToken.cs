using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses.Identity
{
    //AspNetUserTokens
    //[Table("ITS_USER_TOKENS")]
    public class ApplicationUserToken : IdentityUserToken<int>
    {
    }
}
