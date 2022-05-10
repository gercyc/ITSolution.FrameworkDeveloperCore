using ITSolution.Framework.Common.Abstractions.EfOptions;
using ITSolution.Framework.Common.Abstractions.EntityFramework;
using Microsoft.EntityFrameworkCore.Design;

namespace ITSolution.Framework.Common.Abstractions.Identity.DataAccess;

public class ContextFactory : IDesignTimeDbContextFactory<ItsIdentityContext>
{
    public ItsIdentityContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new ItsDbContextOptions();
        return new ItsIdentityContext(optionsBuilder);
    }
}