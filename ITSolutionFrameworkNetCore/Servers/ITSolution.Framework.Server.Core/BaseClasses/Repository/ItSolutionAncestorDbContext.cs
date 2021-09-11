using ITSolution.Framework.Core.BaseClasses.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository
{
    /// <summary>
    /// Ancestor DbContext class. Needs to be inherit.
    /// </summary>
    public abstract class ItSolutionAncestorDbContext : DbContext
    {
        public readonly UserManager<ApplicationUser> UserManager;
        public readonly SignInManager<ApplicationUser> SignInManager;
        public readonly IHttpContextAccessor HttpContextAccessor;
        protected ItSolutionAncestorDbContext(DbContextOptions dbContextOptions, [FromServices]UserManager<ApplicationUser> userManager, [FromServices]SignInManager<ApplicationUser> signInManager, [FromServices] IHttpContextAccessor accessor) : base(dbContextOptions)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            HttpContextAccessor = accessor;
        }
        protected ItSolutionAncestorDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
