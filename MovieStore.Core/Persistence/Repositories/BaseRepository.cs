using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories
{
    public class BaseRepository<TEntity, TContext> : IRepository<TEntity>
      where TContext : DbContext
      where TEntity : class, new()
    {
        protected TContext Context { get; }

        public BaseRepository(TContext context)
        {
            Context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
            return entity;
        }
        public async Task<TEntity?> GetWithInclude(Expression<Func<TEntity, bool>> predicate,
                                params Expression<Func<TEntity, object>>[]? includes)
        {
            IQueryable<TEntity> queryable = Query();
            if (includes != null)
            {
                queryable = includes.Aggregate(queryable, (current, include) => current.Include(include));
            }

            return await queryable.FirstOrDefaultAsync(predicate);
        }


        public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true, CancellationToken cancellationToken = default)
        {

            IQueryable<TEntity> queryable = Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (expression != null) queryable = queryable.Where(expression);

            if (orderBy != null)
            {
                return await orderBy(queryable).ToListAsync(cancellationToken);
            }

            return await queryable.ToListAsync(cancellationToken);
        }
    }
}
