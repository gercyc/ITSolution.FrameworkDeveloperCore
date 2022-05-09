using ITSolution.Framework.Common.Abstractions.EfOptions;
using ITSolution.Framework.Common.Abstractions.EntityFramework;
using ITSolution.Framework.Core.Common.BaseClasses.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618

namespace ITSolution.Framework.Common.Abstractions.Identity.DataAccess
{
    public class ItsIdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ItsIdentityContext(ItsDbContextOptions options) : base(options.DbContextOptions)
        {
            try
            {
                //base.Database.EnsureCreated();
                //base.Database.Migrate();
            }
            catch (Exception)
            {

            }

        }
        /// <summary>
        /// Constructor for EF Migrations
        /// The new instance of ItsDbContextOptions was created using default configurations on ITSolution.Framework.Core.Server\Configuration\ITSConfig.xml
        /// </summary>
        public ItsIdentityContext() : base(new ItsDbContextOptions().DbContextOptions)
        {
            try
            {
                //base.Database.EnsureCreated();

            }
            catch (Exception)
            {

            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            ConfigureUserTable(builder);
            ConfigureRoleTable(builder);
            ConfigureRoleClaimTable(builder);
            ConfigureUserClaimTable(builder);
            ConfigureUserLoginTable(builder);
            ConfigureUserRoleTable(builder);
            ConfigureUserTokenTable(builder);
        }

        private static void ConfigureUserTokenTable(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserToken>()
                                    .ToTable("ITS_USER_TOKENS", "dbo");
            //builder.Entity<ApplicationUserToken>().Property(p => p.UserId).HasColumnType("int");
        }

        private static void ConfigureUserRoleTable(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserRole>()
                                    .ToTable("ITS_USER_ROLES", "dbo");
            //builder.Entity<ApplicationUserRole>().Property(p => p.UserId).HasColumnType("int");
            //builder.Entity<ApplicationUserRole>().Property(p => p.RoleId).HasColumnType("int");
        }

        private static void ConfigureUserLoginTable(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserLogin>()
                                    .ToTable("ITS_USER_LOGINS", "dbo");
            //builder.Entity<ApplicationUserLogin>().Property(p => p.UserId).HasColumnType("int");
            builder.Entity<ApplicationUserLogin>().Property(p => p.LoginProvider).HasColumnType("varchar(200)");
            builder.Entity<ApplicationUserLogin>().Property(p => p.ProviderKey).HasColumnType("varchar(200)");
            builder.Entity<ApplicationUserLogin>().Property(p => p.ProviderDisplayName).HasColumnType("varchar(200)");
        }

        private static void ConfigureUserClaimTable(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserClaim>()
                                    .ToTable("ITS_USER_CLAIMS", "dbo");
            //builder.Entity<ApplicationUserClaim>().Property(p => p.Id).HasColumnType("int");
            builder.Entity<ApplicationUserClaim>().Property(p => p.ClaimType).HasColumnType("varchar(100)");
            builder.Entity<ApplicationUserClaim>().Property(p => p.ClaimValue).HasColumnType("varchar(100)");
        }

        private static void ConfigureRoleClaimTable(ModelBuilder builder)
        {
            builder.Entity<ApplicationRoleClaim>()
                                    .ToTable("ITS_ROLE_CLAIMS", "dbo");
            //builder.Entity<ApplicationRoleClaim>().Property(p => p.Id).HasColumnType("int");
            builder.Entity<ApplicationRoleClaim>().Property(p => p.ClaimType).HasColumnType("varchar(100)");
            builder.Entity<ApplicationRoleClaim>().Property(p => p.ClaimValue).HasColumnType("varchar(100)");
        }

        private static void ConfigureRoleTable(ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>()
                                    .ToTable("ITS_ROLES", "dbo");

            //builder.Entity<ApplicationRole>().Property(p => p.Id).HasColumnType("int");
            builder.Entity<ApplicationRole>().Property(p => p.Name).HasColumnType("varchar(100)");
            builder.Entity<ApplicationRole>().Property(p => p.NormalizedName).HasColumnType("varchar(100)");
        }

        private static void ConfigureUserTable(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                        .ToTable("ITS_USER", "dbo");

            //builder.Entity<ApplicationUser>().Property(p => p.Id).HasColumnType("int");
            builder.Entity<ApplicationUser>().Property(p => p.UserName).HasColumnType("varchar(200)");
            builder.Entity<ApplicationUser>().Property(p => p.NormalizedUserName).HasColumnType("varchar(200)");
            builder.Entity<ApplicationUser>().Property(p => p.Email).HasColumnType("varchar(80)");
            builder.Entity<ApplicationUser>().Property(p => p.NormalizedEmail).HasColumnType("varchar(80)");
            builder.Entity<ApplicationUser>().Property(p => p.PhoneNumber).HasColumnType("varchar(12)");

        }
    }
}
