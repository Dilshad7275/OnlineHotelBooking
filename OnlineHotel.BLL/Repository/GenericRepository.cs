using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.BLL.Repository
{
    public class GenericRepository<T> : IDisposable,IGenericRepository<T> where T :class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T,bool>> filter=null,Func<IQueryable<T>,IOrderedQueryable<T>> orderBy=null,
            string inCludeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach(var includeproperty in inCludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeproperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public T GetT(object id)
        {
            return dbSet.Find(id);
        }
        public async Task<T> GetByIdAsync(object id)
        {
           return await dbSet.FindAsync(id);
        }

      
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
             await  dbSet.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }


        public async Task<T> DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return entity;
        }
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Update(T Entity)
        {
            dbSet.Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;

        }

        public async  Task<T> UpdateAsync(T Entity)
        {
            dbSet.Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
            return Entity;
        }
    }
}
