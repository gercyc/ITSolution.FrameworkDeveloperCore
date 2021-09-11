using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using ITSolution.Framework.Server.Core.BaseEnums;
using ITSolution.Framework.Server.Core.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;
using ITSolution.Framework.Core.BaseClasses.CommonEntities;
using ITSolution.Framework.Core.BaseClasses.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ITSolution.Framework.Servers.Core.FirstAPI.Data
{
    public class DbAccessContext : ItSolutionBaseContext
    {

        /// <summary>
        /// Constructor for EF Migrations
        /// The new instance of ItsDbContextOptions was created using default configurations on ITSolution.Framework.Core.Server\Configuration\ITSConfig.xml
        /// </summary>
        public DbAccessContext() : base(new ItsDbContextOptions())
        {
            base.Database.EnsureCreated();
        }
        public DbAccessContext(ItsDbContextOptions itsDbContextOptions) : base(itsDbContextOptions)
        {
            base.Database.Migrate();
        }
        public DbAccessContext(ItsDbContextOptions itsDbContextOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor accessor) : base(itsDbContextOptions, userManager, signInManager, accessor)
        {
            try
            {
                base.Database.Migrate();

            }
            catch (Exception)
            {
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<ApplicationMenu> MenuSet { get; set; }

        public IITSReporitory<ApplicationMenu, DbAccessContext, string> MenuRep
        {
            get { return new ITSRepository<ApplicationMenu, DbAccessContext, string>(this); }
        }
    }
}