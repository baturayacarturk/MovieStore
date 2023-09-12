using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories
{
    public  interface IRepository<T>:IQuery<T>
    {
        Task<T?> GetWithInclude(Expression<Func<T, bool>> predicate,
                               params Expression<Func<T, object>>[]? includes);
        Task<T?> Get(Expression<Func<T, bool>> predicate);
        Task<IList<T>> GetList(Expression<Func<T, bool>>? expression = null,
                 Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                 Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                 int index = 0, int size = 10,
                 bool enableTracking = true, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
