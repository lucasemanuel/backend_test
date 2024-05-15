using Domain.Repositories;
using Infra.Database;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infra.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public TEntity? GetById(string id)
    {
        return _dbSet.Find(id);
    }

    public IQueryable<TEntity> GetByCriteria(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
        _dbContext.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        _dbContext.SaveChanges();
    }
}
