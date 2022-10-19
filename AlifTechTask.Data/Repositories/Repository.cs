﻿using AlifTechTask.Data.DbContexts;
using AlifTechTask.Data.Helpers;
using AlifTechTask.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AlifTechTask.Data.Repositories
{
    public class Repository<TSource> 
        : IRepository<TSource> where TSource : class
    {
        private readonly AlifTechTaskDbContext _dbContext;
        private readonly DbSet<TSource> _dbSet;
        private readonly string _path;

        public Repository(AlifTechTaskDbContext dbContext, string path) =>
            (_dbContext, _dbSet, _path) = (dbContext, _dbContext.Set<TSource>(), path);
        public async ValueTask<TSource> AddAsync(TSource entity)
        {
            var models = File.ReadAllText(EnvironmentHelper.)

             _dbSet.Add(entity).Entity;

        }
        public async ValueTask DeleteAsync(TSource entity) =>
            _dbSet.Remove(entity);
        public async ValueTask<TSource> GetAsync(Expression<Func<TSource, bool>> expression, 
            string include = null) =>
            await GetAll(expression, include).FirstOrDefaultAsync();
        public async ValueTask<TSource> UpdateAsync(TSource entity) =>
            _dbSet.Update(entity).Entity;
        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression, 
            string include = null, bool isTracking = true)
        {
            var query = expression is null ? _dbSet : _dbSet.Where(expression);

            if(include is not null)
                query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }
        public async ValueTask SaveChangesAsync() =>
            await _dbContext.SaveChangesAsync();
    }
}
