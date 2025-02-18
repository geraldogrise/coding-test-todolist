using Todo.Data.Context;
using Todo.Domain.Aggreagates.Core;
using Todo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Todo.Data.Core.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityCore<TEntity>
    {
        protected DatabaseContext _context;
        protected DbSet<TEntity> _dbSet;

        protected Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Add(IEnumerable<TEntity> entity)
        {
            _dbSet.AddRange(entity);
        }

        public TEntity GetById(params object[] ids)
        {
            return _dbSet.Find(ids);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
        }

        public void Update(TEntity current, TEntity updated)
        {
            _context.Entry(current).CurrentValues.SetValues(updated);
        }

        public void Remove(params object[] ids)
        {
            _context.Remove(GetById(ids));
        }

        public void Remove(TEntity entity)
        {
            _context.Remove(entity);
        }

        public void Remove(IEnumerable<TEntity> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
