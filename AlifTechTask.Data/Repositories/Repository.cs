using AlifTechTask.Data.DbContexts;
using AlifTechTask.Data.IRepositories;
using AlifTechTask.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AlifTechTask.Data.Repositories
{
    public class Repository<TSource> : IRepository<TSource> where TSource : Auditable
    {
        private readonly AlifTechTaskDbContext _dbContext;
        private readonly DbSet<TSource> _dbSet;

        public Repository(AlifTechTaskDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TSource>(); 
        }


        /// <summary>
        /// Add entity to required file
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask<TSource> AddAsync(TSource entity)
            => _dbSet.Add(entity).Entity;


        /// <summary>
        /// Delete a entity from required file by its id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask DeleteAsync(TSource entity)
            => _dbSet.Remove(entity);


        /// <summary>
        /// Select a entity from file by its id
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public async ValueTask<TSource> GetAsync(Expression<Func<TSource, bool>> expression) =>
            await _dbSet.Where(expression).FirstOrDefaultAsync();

      
        /// <summary>
        /// Update a exist entity to entered entity 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask<TSource> UpdateAsync(TSource entity) =>
            _dbSet.Update(entity).Entity;


        /// <summary>
        /// Select all entities from required file
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="include"></param>
        /// <param name="isTracking"></param>
        /// <returns>IQueryable<typeparamref name="TSource"/>></returns>
        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, bool isTracking = true)
        {
            var query = expression is null ? _dbSet : _dbSet.Where(expression);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }


        /// <summary>
        /// Writes a entities to required file
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        public async ValueTask SaveChangesAsync() =>
            await _dbContext.SaveChangesAsync();
    }
}
