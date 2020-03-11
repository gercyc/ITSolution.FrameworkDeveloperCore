using System.Collections.Generic;
using ITSolution.Framework.BaseClasses;
using ITSolution.Framework.Core.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Oracle.ManagedDataAccess.Types;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository
{
    public class ItsDbContextOptions : IDbContextOptions
    {
        public DbContextOptions DbContextOptions { get { return _optionsBuilder.Options; } }
        readonly DbContextOptionsBuilder _optionsBuilder;
        public ItsDbContextOptions()
        {
            _optionsBuilder = new DbContextOptionsBuilder();
            if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                _optionsBuilder.UseSqlServer(EnvironmentManager.Configuration.ConnectionString);
            else if(EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
            {
                ITSOracleConfiguration.ConfigureDataSources();
                _optionsBuilder.UseOracle(EnvironmentManager.Configuration.ConnectionString);
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
