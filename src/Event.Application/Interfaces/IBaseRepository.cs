using Event.Application.Interfaces.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {  
        Task<T> AddAsync(T type);
        Task UpdateAsyncx(T type,
            params Expression<Func<T, object>>[] props);
        Task UpdateAsync(T type, List<string>? includes = null);
        Task<T> GetByAsync(Expression<Func<T, bool>> criteria, string[]? includes = null);
        Task<IEnumerable<T>> GetAllAsync(
                            Expression<Func<T, bool>> criteria = null,
                            string orederByDirection = OrderBy.Ascending,
                            Expression<Func<T, Object>>? orderby = null,
                             string[]? includes = null);
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
        Task DeleteAsync(T entity);

    }
}
