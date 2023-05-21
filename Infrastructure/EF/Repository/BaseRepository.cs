using Application_Core.Common.Identity;
using Application_Core.Common.Specification;
using Infrastructure.Database;
using Infrastructure.EF.Evaluator;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repository;

public abstract class BaseRepository<TEntity,TKey> : IRepositoryBase<TEntity> where TEntity: class, IUidIdentity<TKey> where TKey: IEquatable<TKey>
{
    protected readonly ImageSharingDbContext Context;

    protected BaseRepository(ImageSharingDbContext context)
    {
        Context = context;
    }
    
    public virtual async Task<IEnumerable<TEntity>> GetByCriteria(ISpecification<TEntity> criteria)
    {
        return await GetByCriteriaQuery(criteria).ToListAsync();
    }
    
    public virtual IQueryable<TEntity> GetByCriteriaQuery(ISpecification<TEntity> criteria)
    {
        return SpecificationToQueryEvaluator<TEntity>.ApplySpecification(
            Context.Set<TEntity>(),
            criteria
        );
    }

    public virtual async Task<TEntity?> GetByCriteriaSingle(ISpecification<TEntity> criteria)
    {
        return await SpecificationToQueryEvaluator<TEntity>.ApplySpecification(
            Context.Set<TEntity>(),
            criteria
        ).FirstOrDefaultAsync();
    }

    public virtual async Task<TEntity?> GetByGuid(Guid id)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(e => e.Guid == id);
    }

    public virtual async Task Add(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task Remove(TEntity entity)
    {
        await Task.FromResult(Context.Set<TEntity>().Remove(entity));
        await Context.SaveChangesAsync();
    }

}