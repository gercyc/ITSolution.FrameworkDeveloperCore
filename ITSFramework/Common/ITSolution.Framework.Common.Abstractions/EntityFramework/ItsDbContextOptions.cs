using ITSolution.Framework.Common.Abstractions.EfOptions;
using ITSolution.Framework.Common.BaseClasses.EnvironmentConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
#pragma warning disable CS8603

namespace ITSolution.Framework.Common.Abstractions.EntityFramework
{
    public class ItsDbContextOptions : IDbContextOptions
    {
        public DbContextOptions DbContextOptions { get { return _optionsBuilder.Options; } }
        readonly DbContextOptionsBuilder _optionsBuilder;
        public ItsDbContextOptions()
        {
            _optionsBuilder = new DbContextOptionsBuilder();
            if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                _optionsBuilder.UseSqlServer(EnvironmentInformation.ConnectionString, options => options.MigrationsAssembly("ITSolution.Bootstraper"));
            else if(EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
            {
                ItsOracleConfiguration.ConfigureDataSources();
                _optionsBuilder.UseOracle(EnvironmentInformation.ConnectionString);
            }
            else if(EnvironmentInformation.DatabaseType == DatabaseType.SQLITE)
            {
                _optionsBuilder.UseSqlite(EnvironmentInformation.ConnectionString);
            }
        }

        public IEnumerable<IDbContextOptionsExtension> Extensions
        {
            get
            {
                return _optionsBuilder.Options.Extensions;
            }
        }

        TExtension IDbContextOptions.FindExtension<TExtension>()
        {
            return _optionsBuilder.Options.FindExtension<TExtension>();
        }
    }
}
