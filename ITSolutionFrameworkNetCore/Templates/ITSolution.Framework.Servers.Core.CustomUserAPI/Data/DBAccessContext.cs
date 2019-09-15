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

namespace ITSolution.Framework.Core.CustomUserAPI.Data
{
    /// <summary>
    /// Customize your context
    /// </summary>
    public class DBAccessContext : ITSolutionContext
    {

        public DBAccessContext(ITSDbContextOptions itsDbContextOptions) : base(itsDbContextOptions)
        {
            this.ITSDbContextOptions = itsDbContextOptions;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //public DbSet<YourEntity> EntitySet { get; set; }
        //public IITSReporitory<YourEntity, DBAccessContext> EntityRep { get { return new ITSRepository<YourEntity, DBAccessContext>(this); } }
    }
}
