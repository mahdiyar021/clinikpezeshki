using clinikpezeshki.Models.Entitys;
using Microsoft.EntityFrameworkCore;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : DomainEntity
{
    private readonly IUnitOfWork _IUnitOfWork;
    private readonly DbSet<TEntity> _DbSet;
    protected GenericRepository(IUnitOfWork unitOfWork)
    {
        _IUnitOfWork = unitOfWork;
        _DbSet = _IUnitOfWork.Set<TEntity>();
    }

    public TEntity? Find(int id)
    {
      return _DbSet.Find(id);
    }

    public async Task<TEntity?> FindAsync(int id)
    {
        return await _DbSet.FindAsync(id);
    }

    public async Task<TEntity?> FindAsync(int id,CancellationToken cancellationToken)
    {
        return await _DbSet.FindAsync(id,cancellationToken);
    }

    public int Add(TEntity entity)
    {
        _DbSet.Add(entity);

        _IUnitOfWork.SaveChange();

        return entity.Id;
    }

    public async Task<int> AddAsync(TEntity entity)
    {
        await _DbSet.AddAsync(entity);

        await _IUnitOfWork.SaveChangeAsync();

        return entity.Id;
    }

    public async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _DbSet.AddAsync(entity, cancellationToken);

        await _IUnitOfWork.SaveChangeAsync(cancellationToken);

        return entity.Id;
    }

    public bool Delete(TEntity entity)
    {
        var FoundedEntity = _DbSet.Find(entity.Id);

        if (FoundedEntity != null)
        {
            _DbSet.Remove(entity);
            _IUnitOfWork.SaveChange();
            return true;
        }

        return false;

    }

    public bool Delete(int id)
    {
        var FoundedEntity = _DbSet.Find(id);

        if (FoundedEntity != null)
        {
            _DbSet.Remove(FoundedEntity);

            _IUnitOfWork.SaveChange();
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        var FoundedEntity = await _DbSet.FindAsync(entity.Id);

        if (FoundedEntity != null)
        {
            _DbSet.Remove(entity);

            await _IUnitOfWork.SaveChangeAsync();

            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var FoundedEntity = await _DbSet.FindAsync(entity.Id, cancellationToken);

        if (FoundedEntity != null)
        {
            _DbSet.Remove(entity);

            await _IUnitOfWork.SaveChangeAsync(cancellationToken);

            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var FoundedEntity = await _DbSet.FindAsync(id);

        if (FoundedEntity != null)
        {
            _DbSet.Remove(FoundedEntity);

            await _IUnitOfWork.SaveChangeAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var FoundedEntity = await _DbSet.FindAsync(id, cancellationToken);

        if (FoundedEntity != null)
        {
            _DbSet.Remove(FoundedEntity);

            await _IUnitOfWork.SaveChangeAsync(cancellationToken);

            return true;
        }

        return false;
    }

    public IQueryable<TEntity> GetAll()
    {
        return  _DbSet.AsQueryable();
    }


    public void Update(TEntity entityToUpdate)
    {
        try
        {
            var entry = _IUnitOfWork.Entry(entityToUpdate);

            var attachedEntity = _DbSet.Find(entityToUpdate.Id);

            if (attachedEntity != null)
            {
                var attachedEntry = _IUnitOfWork.Entry(attachedEntity);

                attachedEntry.CurrentValues.SetValues(entityToUpdate);

                attachedEntry.State = EntityState.Modified;

            }
            else
            {
                entry.State = EntityState.Modified;

            }


        }
        catch
        {
            _DbSet.Attach(entityToUpdate);

            _IUnitOfWork.Entry(entityToUpdate).State = EntityState.Modified;

        }

        _IUnitOfWork.SaveChange();
    }
}

