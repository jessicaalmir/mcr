using System.Linq.Expressions;
using mcr.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace mcr.Data
{
    public class Repository<Tid, TEntity> : IRepository<Tid, TEntity>
    where Tid : struct
    where TEntity : BaseEntity<Tid>
    {
        internal DataContext _context;
        internal DbSet<TEntity> _dbSet;

        public Repository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> FindAsync(Tid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            IEnumerable<Expression<Func<TEntity, object>>> includes = null,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

             if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy is not null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual async Task Delete(Tid id)
        {
            TEntity entitToDetelete = await _dbSet.FindAsync(id);
            Delete(entitToDetelete);


        }

        public virtual async Task Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}