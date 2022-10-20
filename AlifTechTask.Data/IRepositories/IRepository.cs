using System.Linq.Expressions;

namespace AlifTechTask.Data.IRepositories
{
    public interface IRepository<TSourse>
    {
        ValueTask<TSourse> AddAsync(TSourse entity);
        ValueTask<TSourse> UpdateAsync(TSourse entity);
        ValueTask DeleteAsync(TSourse entity);
        ValueTask<TSourse> GetAsync(Expression<Func<TSourse, bool>> expression);
        IQueryable<TSourse> GetAll(Expression<Func<TSourse, bool>> expression = null, bool isTracking = true);
        ValueTask SaveChangesAsync();
    }
}
