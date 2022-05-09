using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using ITSolution.Framework.Server.Core.BaseEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ITSolution.Framework.Common.Abstractions.EfOptions;
using ITSolution.Framework.Common.Abstractions.EntityFramework;
using ITSolution.Framework.Common.Abstractions.EntityFramework.Context;
using ITSolution.Framework.Core.Common.BaseClasses.AbstractEntities;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;
using ITSolution.Framework.Core.Server.BaseInterfaces;

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