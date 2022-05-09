using ITSolution.Framework.Common.Abstractions.EfOptions;
using ITSolution.Framework.Core.Common.BaseClasses.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
#pragma warning disable CS8618

namespace ITSolution.Framework.Common.Abstractions.EntityFramework.Context
{
    public class ItSolutionBaseContext : ItSolutionAncestorDbContext
    {
        public ItsDbContextOptions ItsDbContextOptions { get; private set; }

        protected ItSolutionBaseContext(ItsDbContextOptions dbContextOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor accessor) : base(dbContextOptions.DbContextOptions, userManager, signInManager, accessor)
        {
            this.ItsDbContextOptions = dbContextOptions;
        }
        protected ItSolutionBaseContext(ItsDbContextOptions dbContextOptions) : base(dbContextOptions.DbContextOptions)
        {
            this.ItsDbContextOptions = dbContextOptions;
        }
        public ItSolutionBaseContext() : base(new ItsDbContextOptions().DbContextOptions)
        {
            
        }
    }
}
