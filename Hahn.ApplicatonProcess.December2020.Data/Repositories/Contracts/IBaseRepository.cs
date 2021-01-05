using Hahn.ApplicatonProcess.December2020.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repositories.Contracts
{
    public interface IBaseRepository<TEntity, PK> where TEntity : Entity<PK>
    {
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task<TEntity> Get(object id);
        Task<TEntity> Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(object id);
        Task Delete(TEntity entity);
    }
}
