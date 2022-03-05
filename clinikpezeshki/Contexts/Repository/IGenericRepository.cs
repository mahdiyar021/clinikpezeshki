using clinikpezeshki.Models.Entitys;

public interface IGenericRepository<TEntity> where TEntity : DomainEntity
{
    public TEntity? Find(int id);
    public Task<TEntity?> FindAsync(int id);
    public Task<TEntity?> FindAsync(int id, CancellationToken cancellationToken);
    public int Add(TEntity entity);
    public Task<int> AddAsync(TEntity entity);
    public Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken);
    public bool Delete(TEntity entity);
    public Task<bool> DeleteAsync(TEntity entity);
    public Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    public bool Delete(int id);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    public IQueryable<TEntity> GetAll();
    public void Update(TEntity entityToUpdate);

}

