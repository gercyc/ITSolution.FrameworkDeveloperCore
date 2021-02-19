using ITSolution.Framework.Core.BaseClasses.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository
{
    /// <summary>
    /// Ancestor DbContext class. Needs to be inherit.
    /// </summary>
    public abstract class ItSolutionAncestorDbContext : DbContext
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public SignInManager<ApplicationUser> SignInManager { get; private set; }
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
        protected ItSolutionAncestorDbContext(DbContextOptions dbContextOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor accessor) : base(dbContextOptions)
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
