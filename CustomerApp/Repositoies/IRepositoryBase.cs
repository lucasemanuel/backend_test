namespace CustomerApp.Repositories;
public interface IRepositoryBase<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetById(string id);
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}