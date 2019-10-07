using Microsoft.EntityFrameworkCore;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository
{
    public class ItSolutionBaseContext : ItSolutionAncestorDbContext
    {
        public ItsDbContextOptions ItsDbContextOptions { get; private set; }

        protected ItSolutionBaseContext(ItsDbContextOptions dbContextOptions) : base(dbContextOptions.DbContextOptions)
        {
            this.ItsDbContextOptions = dbContextOptions;
        }
    }
}
