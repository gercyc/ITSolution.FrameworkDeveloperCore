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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            builder.Entity<ApplicationRole>()
                        .ToTable("ITS_ROLES", "dbo");

            builder.Entity<ApplicationRoleClaim>()
                        .ToTable("ITS_ROLE_CLAIMS", "dbo");

            builder.Entity<ApplicationUser>()
                        .ToTable("ITS_USER", "dbo");

            builder.Entity<ApplicationUserClaim>()
                        .ToTable("ITS_USER_CLAIMS", "dbo");

            builder.Entity<ApplicationUserLogin>()
                        .ToTable("ITS_USER_LOGINS", "dbo");

            builder.Entity<ApplicationUserRole>()
                        .ToTable("ITS_USER_ROLES", "dbo");

            builder.Entity<ApplicationUserToken>()
                        .ToTable("ITS_USER_TOKENS", "dbo");
        }
    }
}
