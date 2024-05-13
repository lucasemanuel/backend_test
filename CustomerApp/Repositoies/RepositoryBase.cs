using CustomerApp.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CustomerApp.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;

    public RepositoryBase(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>().ToList();
    }

    public TEntity? GetById(string id)
    {
        return _dbContext.Set<TEntity>().Find(id);
    }

    public void Insert(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        _dbContext.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        _dbContext.SaveChanges();
    }
}
