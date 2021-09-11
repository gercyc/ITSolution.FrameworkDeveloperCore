using ITSolution.Framework.Core.BaseClasses.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository
{
    public class ItSolutionBaseContext : ItSolutionAncestorDbContext
    {
        public ItsDbContextOptions ItsDbContextOptions { get; private set; }
        public ItSolutionBaseContext() : base(new ItsDbContextOptions().DbContextOptions)
        {
            this.ItsDbContextOptions = new ItsDbContextOptions();
        }
        protected ItSolutionBaseContext(ItsDbContextOptions dbContextOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor accessor) : base(dbContextOptions.DbContextOptions, userManager, signInManager, accessor)
        {
            this.ItsDbContextOptions = dbContextOptions;
        }
        protected ItSolutionBaseContext(ItsDbContextOptions dbContextOptions) : base(dbContextOptions.DbContextOptions)
        {
            this.ItsDbContextOptions = dbContextOptions;
        }
    }
}
