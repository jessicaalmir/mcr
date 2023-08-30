using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using mcr.Data.Models;

namespace mcr.Data
{
    public interface IRepository<Tid, TEntity> 
    where Tid : struct 
    where TEntity:BaseEntity<Tid>
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> FindAsync(Tid id);
        Task<IEnumerable<TEntity>> GetAllAsync(
            IEnumerable<Expression<Func<TEntity, object>>> includes = null,
            Expression<Func<TEntity, bool>> filter =  null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(Tid id);
    }
}