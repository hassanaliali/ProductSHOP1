using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Core.Interfaces;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
    //public class GenericRepository <T>: IGenericRepository <T> where T : class
    //{
    //    public GenericRepository(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public ApplicationDbContext _context { get; }

    //    public async Task AddAsync(T entity)
    //    {
    //        await _context.Set<T>().AddAsync(entity);
    //        await _context.SaveChangesAsync();
    //    }

    //    public async Task DeleteAsync(int id)
    //    {

    //        var Entity = await _context.Set<T>().FindAsync(id);
    //        _context.Set<T>().Remove(Entity);
    //        await _context.SaveChangesAsync();
    //    }

    //    public IEnumerable<T> GetAll()
    //    => _context.Set<T>().AsNoTracking().ToList();


    //    //public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, bool>>[] includes)
    //    //=>await _context.Set<T>().AsNoTracking().ToListAsync();

    //    //public async Task<IReadOnlyList<T>> GetAllAsync()
    //    //=> await _context.Set<T>().AsNoTracking().ToListAsync();

    //    //public async Task<IEnumerable<T>> GetALLAsync(params Expression<Func<T, bool>>[] includes)
    //    //{
    //    //    var query = _context.Set<T>().AsQueryable();
    //    //    foreach(var item in includes)
    //    //    {
    //    //        query = query.Include(item);
    //    //    }
    //    //    return await query.ToListAsync();
    //    //}

    //    //public async Task<T> GetAsync(int id)
    //    //=> await _context.Set<T>().FindAsync(id);


    //    //public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    //    //{
    //    //    IQueryable<T> query = _context.Set<T>();
    //    //    foreach(var item in includes)
    //    //    {
    //    //        query = query.Include(item);
    //    //    }
    //    //    return await ((DbSet<T>)query).FindAsync(id);
    //    //}

    //    //public async Task UpdateAsync(int id, T entity)
    //    //{
    //    //    var Entity = await _context.Set<T>().FindAsync(id);
    //    //    if(Entity is not null)
    //    //    {
    //    //    _context.Set<T>().Update(entity);
    //    //    await _context.SaveChangesAsync();

    //    //    }
    //    //}

    //    //IEnumerable<T> IGenericRepository<T>.GetAll(params Expression<Func<T, bool>>[] includes)
    //    // => _context.Set<T>().AsNoTracking().ToList();
    //}
}
