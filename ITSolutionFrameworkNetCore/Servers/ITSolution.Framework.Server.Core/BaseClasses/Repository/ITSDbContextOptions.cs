using ITSolution.Framework.BaseClasses;
using ITSolution.Framework.Core.BaseClasses;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSolution.Framework.Server.Core.BaseClasses.Repository
{
    public class ITSDbContextOptions : IDbContextOptions
    {
        public DbContextOptions DbContextOptions { get { return optionsBuilder.Options; } }
        DbContextOptionsBuilder optionsBuilder;
        public ITSDbContextOptions()
        {
            optionsBuilder = new DbContextOptionsBuilder();
            if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                optionsBuilder.UseSqlServer(EnvironmentManager.Configuration.ConnectionString);
            else
            {
                ITSOracleConfiguration.ConfigureDataSources();
                optionsBuilder.UseOracle(EnvironmentManager.Configuration.ConnectionString);
            }
        }

        public IEnumerable<IDbContextOptionsExtension> Extensions
        {
            get
            {
                return optionsBuilder.Options.Extensions;
            }
        }

        TExtension IDbContextOptions.FindExtension<TExtension>()
        {
            return optionsBuilder.Options.FindExtension<TExtension>();
        }
    }
}
