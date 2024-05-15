using System.Linq.Expressions;

namespace Domain.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetById(string id);
    IQueryable<TEntity> GetByCriteria(Expression<Func<TEntity, bool>> predicate);
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}