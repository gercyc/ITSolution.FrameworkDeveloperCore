using ITSolution.Framework.Common.BaseClasses.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace ITSolution.Framework.Common.Abstractions.EntityFramework.Context
{
    /// <summary>
    /// Ancestor DbContext class. Needs to be inherit.
    /// </summary>
    public abstract class ItSolutionAncestorDbContext : DbContext
    {
        private readonly DbContextOptions _dbContextOptions;
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
            _dbContextOptions = dbContextOptions;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
