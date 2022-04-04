using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Taallama.Data.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        Task<IQueryable<T>> Where(Expression<Func<T, bool>> expression = null);
    }
}