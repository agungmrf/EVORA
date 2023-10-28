using API.Data;

namespace API.Contracts;

public interface IGeneralRepository<TEntity>
{
    EvoraDbContext GetContext();
    IEnumerable<TEntity> GetAll();
    TEntity? GetByGuid(Guid guid);
    TEntity? Create(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
}