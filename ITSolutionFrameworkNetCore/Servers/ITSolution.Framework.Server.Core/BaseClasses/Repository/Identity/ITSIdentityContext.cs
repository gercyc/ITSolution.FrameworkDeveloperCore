using ITSolution.Framework.BaseClasses;
using ITSolution.Framework.Core.BaseClasses;
using ITSolution.Framework.Core.BaseClasses.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository.Identity
{
    public class ITSIdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ITSIdentityContext(ItsDbContextOptions options) : base(options.DbContextOptions)
        {
            //if(EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
            base.Database.EnsureCreated();
        }
        public ITSIdentityContext() : base(new ItsDbContextOptions().DbContextOptions)
        {
            //if(EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
            base.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            string owner = "GERCY";
            builder.Entity<ApplicationRole>()
                        .ToTable("ITS_ROLES", owner);

            builder.Entity<ApplicationRoleClaim>()
                        .ToTable("ITS_ROLE_CLAIMS", owner);

            builder.Entity<ApplicationUser>()
                        .ToTable("ITS_USER", owner);

            builder.Entity<ApplicationUserClaim>()
                        .ToTable("ITS_USER_CLAIMS", owner);

            builder.Entity<ApplicationUserLogin>()
                        .ToTable("ITS_USER_LOGINS", owner);

            builder.Entity<ApplicationUserRole>()
                        .ToTable("ITS_USER_ROLES", owner);

            builder.Entity<ApplicationUserToken>()
                        .ToTable("ITS_USER_TOKENS", owner);
        }
    }
}
