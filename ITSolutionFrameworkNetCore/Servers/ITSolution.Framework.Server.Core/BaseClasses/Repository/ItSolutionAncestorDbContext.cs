using Microsoft.EntityFrameworkCore;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository
{
    /// <summary>
    /// Ancestor DbContext class. Needs to be inherit.
    /// </summary>
    public abstract class ItSolutionAncestorDbContext : DbContext
    {
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
