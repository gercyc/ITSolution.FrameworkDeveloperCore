using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
    }
}
