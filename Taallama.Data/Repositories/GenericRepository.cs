using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Taallama.Data.Contexts;
using Taallama.Data.IRepositories;

#pragma warning disable
namespace Taallama.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected TaallamaDbContext dbContext;
        protected DbSet<T> dbSet;

        public GenericRepository(TaallamaDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity) =>
            (await dbSet.AddAsync(entity)).Entity;

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            T entity = await dbSet.FirstOrDefaultAsync(expression);

            if (entity == null)
                return false;

            dbSet.Remove(entity);

            return true;
        }

        public async Task<IQueryable<T>> Where(Expression<Func<T, bool>> expression = null) =>
            expression is null ? dbSet : dbSet.Where(expression);

        public Task<T> GetAsync(Expression<Func<T, bool>> expression) =>
            dbSet.FirstOrDefaultAsync(expression);

        public async Task<T> UpdateAsync(T entity) =>
            dbSet.Update(entity).Entity;
    }
}