using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BaseUseCase
{
    public abstract class BaseUseCase<TEntity, TRepository>
        where TEntity : class
        where TRepository : IRepository<TEntity>
    {
        protected readonly TRepository Repository;

        protected BaseUseCase(TRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
      
    }
}
