using ITSolution.Framework.Common.Abstractions.EntityFramework;
using Microsoft.EntityFrameworkCore.Design;

namespace ITSolution.Framework.Servers.Core.FirstAPI.Data;

public class ContextFactory : IDesignTimeDbContextFactory<DbAccessContext>
{
    public DbAccessContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new ItsDbContextOptions();
        return new DbAccessContext(optionsBuilder);
    }
}