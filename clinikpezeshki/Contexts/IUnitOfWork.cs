using clinikpezeshki.Models.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IUnitOfWork
{
    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    public int SaveChange();
    public Task<int> SaveChangeAsync();
    public Task<int> SaveChangeAsync(CancellationToken cancellationToken);
    public DbSet<TEntity> Set<TEntity>() where TEntity : DomainEntity;
}

