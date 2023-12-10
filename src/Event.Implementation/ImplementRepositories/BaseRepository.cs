using AutoMapper.Internal;
using Event.Application.Interfaces;
using Event.Application.Interfaces.Const;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Event.Implementation.ImplementRepositories
{
    public sealed class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
      

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
         
        }

        public async Task<T> AddAsync(T type)
        {
           await _context.Set<T>().AddAsync(type);
           await _context.SaveChangesAsync();
           return type;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().AsNoTracking().CountAsync(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().AsNoTracking().CountAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> criteria = null,
            string orederByDirection = OrderBy.Ascending, 
            Expression<Func<T, object>>? orderby = null, 
            string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (criteria != null)
                query = query.Where(criteria);
            if (orderby != null)
            {
                if (orederByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderby);
                else
                    query = query.OrderByDescending(orderby);
            }

            //if (skip.HasValue && take.HasValue)
            //    query = query.Skip(skip.Value).Take(take.Value);


            if (includes != null)
                foreach (var inc in includes)
                    query = query.Include(inc);
            return await query.ToListAsync();
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (includes != null)
                foreach (var inc in includes)
                    query = query.Include(inc);
            return await query.SingleOrDefaultAsync(criteria);
        }
        public async Task UpdateAsyncx(T type, 
            params Expression<Func<T,object>>[] props)
        {
            var entry = _context.Entry(type);
            _context.Set<T>().Attach(type);
            
            foreach(var p in props)
                entry.Property(p).IsModified= true;
            await _context.SaveChangesAsync();
        }
        
            public async Task UpdateAsync(T type, List<string>? includes = null)
        {

            var entry = _context.Entry(type);
            _context.Set<T>().Attach(type);

            foreach (var p in includes)
                entry.Property(p).IsModified = true;
            await _context.SaveChangesAsync();

;
            
            
        }
    }
}
