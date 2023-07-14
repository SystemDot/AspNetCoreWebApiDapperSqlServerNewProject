namespace NewProject.WebApi.Domain;

public interface IRepository<TEntity>
{
    public Task<TEntity> GetAsync(string id);

    public Task SaveAsync(TEntity entity);
}