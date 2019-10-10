using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using ITSolution.Framework.Server.Core.BaseEnums;
using ITSolution.Framework.Server.Core.BaseInterfaces;
using ITSolution.Framework.Servers.Core.FirstAPI.Model;
using ITSolution.Framework.Servers.Core.FirstAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;

namespace ITSolution.Framework.Servers.Core.FirstAPI.Data
{
    public class DbAccessContext : ItSolutionBaseContext
    {
        public DbAccessContext(ItsDbContextOptions itsDbContextOptions) : base(itsDbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Menu> MenuSet { get; set; }

        public IITSReporitory<Menu, DbAccessContext> MenuRep
        {
            get { return new ITSRepository<Menu, DbAccessContext>(this); }
        }

        public DbSet<Usuario> UsuarioSet { get; set; }

        public IITSReporitory<Usuario, DbAccessContext> UsuarioRep
        {
            get { return new ITSRepository<Usuario, DbAccessContext>(this); }
        }
    }
}