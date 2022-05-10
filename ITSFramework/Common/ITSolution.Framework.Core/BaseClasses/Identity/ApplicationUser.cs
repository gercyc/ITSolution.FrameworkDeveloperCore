using System;
using Microsoft.AspNetCore.Identity;

namespace ITSolution.Framework.Common.BaseClasses.Identity
{
    //AspNetUsers
    //[Table("ITS_USER")]
    public sealed class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
            SecurityStamp = Guid.NewGuid().ToString();
        }
    }
}
