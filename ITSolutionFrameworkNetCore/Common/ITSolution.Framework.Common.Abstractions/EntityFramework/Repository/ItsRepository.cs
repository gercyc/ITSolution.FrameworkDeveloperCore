using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using ITSolution.Framework.Common.Abstractions.EntityFramework.Context;
using ITSolution.Framework.Common.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8625,CS8603,CS8602

namespace ITSolution.Framework.Common.Abstractions.EntityFramework.Repository
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="TEntity">Entidade</typeparam>
    /// <typeparam name="TContext">Contexto</typeparam>
    /// <typeparam name="TPk">Tipo de chave</typeparam>
    public class ItsRepository<TEntity, TContext, TPk> : IItsReporitory<TEntity, TContext, TPk>
        where TEntity : class, IEntity<TPk>
        where TContext : ItSolutionAncestorDbContext
        where TPk : IEquatable<TPk>
    {
        #region Private properties
        private readonly TContext _context;
        #endregion

        #region Constructor
        public ItsRepository(TContext context)
        {
            _context = context;
        }
        #endregion

        #region Public methods
        /// <inheritdoc/>
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderBy = null,
            OrderDirection orderDirection = OrderDirection.Ascending,
            int skip = 0,
            int take = 0,
            Expression<Func<TEntity, object>>[] includes = null)
        {
            return GetQuery(predicate, orderBy, orderDirection, skip, take, includes)
                .ToList();
        }

        /// <inheritdoc />

        /// <inheritdoc/>
        public IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderBy = null,
            OrderDirection orderDirection = OrderDirection.Ascending,
            int skip = 0,
            int take = 0,
            Expression<Func<TEntity, object>>[] includes = null)
        {
            return GetQuery(predicate, orderBy, orderDirection, skip, take, includes)
                .AsQueryable();
        }

        /// <inheritdoc />

        /// <inheritdoc/>
        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderBy = null,
            OrderDirection orderDirection = OrderDirection.Ascending,
            int skip = 0,
            int take = 0,
            Expression<Func<TEntity, object>>[] includes = null)
        {
            return await GetQuery(predicate, orderBy, orderDirection, skip, take)
                .ToListAsync();
        }

        /// <inheritdoc />
        public TEntity FirstOrDefault<TKey>(TKey key)
        {
            return _context
                .Set<TEntity>()
                .Find(key);
        }

        /// <inheritdoc />
        public async Task<TEntity> FirstOrDefaultAsync<TKey>(TKey key)
        {
            return await _context
                .Set<TEntity>()
                .FindAsync(key);
        }

        /// <inheritdoc />
        public void Update(TEntity entity, bool saveChanges = false)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            if (_context.SignInManager.IsSignedIn(_context.SignInManager.Context.User))
            {
                entity.ModifiedBy = Convert.ToInt32(_context.SignInManager.Context.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            }

            var set = _context.Set<TEntity>();
            set.Attach(entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;

            if (saveChanges)
            {
                SaveChanges();
            }
        }

        /// <inheritdoc />
        public async Task UpdateAsync(TEntity entity, bool saveChanges = false)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            if (_context.SignInManager.Context.User.Identity.IsAuthenticated)
            {
                entity.ModifiedBy = Convert.ToInt32(_context.SignInManager.Context.User.Claims.Where(c => c.Type == "jti").FirstOrDefault().Value);
            }
            var set = _context
                .Set<TEntity>();

            set.Attach(entity);

            var entry = _context
                .Entry(entity);

            entry.State = EntityState.Modified;

            if (saveChanges)
            {
                await SaveChangesAsync();
            }
        }

        /// <summary>
        /// <see cref="ItsRepository{TEntity,TContext,TPk}.Create(TEntity, bool)"/>
        /// </summary>
        public void Create(TEntity entity, bool saveChanges = false)
        {
            entity.CreatedTimestamp = DateTime.UtcNow;

            try
            {
                Type propType = entity.GetType().BaseType.GetProperty("Id").PropertyType;

                if (propType.Name == "String")
                    entity.Id = (TPk)Convert.ChangeType(Guid.NewGuid().ToString(), propType);
                else if (propType.Name == "Int32")
                    entity.Id = (TPk)Convert.ChangeType(NextVal(entity), propType);
            }
            catch (Exception)
            {
                entity.Id = (TPk)Convert.ChangeType(Guid.NewGuid().ToString(), typeof(string));
            }

            if ((_context.HttpContextAccessor.HttpContext != null) && _context.SignInManager.IsSignedIn(_context.HttpContextAccessor.HttpContext.User))
            {
                entity.CreatedBy = Convert.ToInt32(_context.SignInManager.Context.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            }

            var set = _context
                .Set<TEntity>();

            set.Add(entity);

            if (saveChanges)
            {
                SaveChanges();
            }
        }
        public int NextVal(TEntity entity)
        {
            var atrTable = entity.GetType().GetCustomAttribute<TableAttribute>() != null ? entity.GetType().GetCustomAttribute<TableAttribute>().Name : entity.GetType().Name;

            DbConnection conn = _context.Database.GetDbConnection();
            DbCommand command = conn.CreateCommand();
            command.CommandText = string.Format("SELECT NEXT VALUE FOR SEQ_{0}_ID", atrTable);

            if (conn.State != System.Data.ConnectionState.Open && conn.State != System.Data.ConnectionState.Connecting)
                conn.Open();

            return Convert.ToInt32(command.ExecuteScalar());
        }

        /// <summary>
        /// <see cref="ItsRepository{TEntity,TContext,TPk}.CreateAsync(TEntity, bool)"/>
        /// </summary>
        public async Task CreateAsync(TEntity entity, bool saveChanges = false)
        {
            entity.CreatedTimestamp = DateTime.UtcNow;

            //try
            //{
            //    Type propType = entity.GetType().BaseType.GetProperty("Id").PropertyType;

            //    if (propType.Name == "String")
            //        entity.Id = (Tpk)Convert.ChangeType(Guid.NewGuid().ToString(), propType);
            //    else if (propType.Name == "Int32")
            //        entity.Id = (Tpk)Convert.ChangeType(NextVal(entity), propType);
            //}
            //catch (Exception)
            //{
            //    entity.Id = (Tpk)Convert.ChangeType(Guid.NewGuid().ToString(), typeof(string));
            //}

            if (_context.SignInManager.IsSignedIn(_context.SignInManager.Context.User))
            {
                entity.CreatedBy = Convert.ToInt32(_context.SignInManager.Context.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            }

            var set = _context
                .Set<TEntity>();

            await set
                 .AddAsync(entity);

            if (saveChanges)
            {
                await SaveChangesAsync();
            }
        }

        /// <summary>
        /// <see cref="ItsRepository{TEntity,TContext,TPk}.Delete(TEntity, bool)"/>
        /// </summary>
        public void Delete(TEntity entity, bool saveChanges = false)
        {
            _context.Remove(entity);

            if (saveChanges)
            {
                SaveChanges();
            }
        }

        /// <summary>
        /// <see cref="ItsRepository{TEntity,TContext,TPk}.DeleteAsync(TEntity, bool)"/>
        /// </summary>
        public async Task DeleteAsync(TEntity entity, bool saveChanges = false)
        {
            _context.Remove(entity);

            if (saveChanges)
            {
                await SaveChangesAsync();
            }
        }

        /// <summary>
        /// <see cref="ItsRepository{TEntity,TContext,TPk}.SaveChanges"/>
        /// </summary>
        public void SaveChanges()
        {
            _context
                .SaveChanges();
        }

        /// <summary>
        /// <see cref="ItsRepository{TEntity,TContext,TPk}.SaveChangesAsync"/>
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context
                .SaveChangesAsync();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Retreive a queryable
        /// </summary>
        /// <param name="predicate">Filter the results.</param>
        /// <param name="orderBy">Order the results.</param>
        /// <param name="orderDirection">Specify the direction of the ordering.</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="includes">An array of navigation property includes.</param>
        /// <returns></returns>
        private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderBy = null,
            OrderDirection orderDirection = OrderDirection.Ascending,
            int skip = 0,
            int take = 0,
            Expression<Func<TEntity, object>>[] includes = null)
        {
            var querySet = _context
                 .Set<TEntity>();

            var query = querySet
                 .AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                if (orderDirection == OrderDirection.Ascending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take > 0)
            {
                query = query.Take(take);
            }


            return query;
        }
        #endregion

    }
}
