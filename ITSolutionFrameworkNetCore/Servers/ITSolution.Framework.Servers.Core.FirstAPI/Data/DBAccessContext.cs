using ITSolution.Framework.Common.Abstractions.EntityFramework;
using ITSolution.Framework.Common.Abstractions.EntityFramework.Context;
using ITSolution.Framework.Common.Abstractions.EntityFramework.Repository;
using ITSolution.Framework.Common.BaseClasses.AbstractEntities;
using Microsoft.EntityFrameworkCore;

namespace ITSolution.Framework.Servers.Core.FirstAPI.Data
{
    public class DbAccessContext : ItSolutionBaseContext
    {
        public DbAccessContext(ItsDbContextOptions itsDbContextOptions) : base(itsDbContextOptions)
        {
            base.Database.Migrate();
        }
        /// <summary>
        /// Constructor for EF Migrations
        /// The new instance of ItsDbContextOptions was created using default configurations on ITSolution.Framework.Core.Server\Configuration\ITSConfig.xml
        /// </summary>
        public DbAccessContext() : base(new ItsDbContextOptions())
        {
            base.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<ApplicationMenu> MenuSet { get; set; }

        public IItsReporitory<ApplicationMenu, DbAccessContext, string> MenuRep
        {
            get { return new ItsRepository<ApplicationMenu, DbAccessContext, string>(this); }
        }
        
    }
}