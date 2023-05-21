using Application_Core.Common.Identity;
using Application_Core.Common.Specification;
using Infrastructure.Database;
using Infrastructure.EF.Evaluator;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repository;

public abstract class BaseRepository<TEntity,TKey> : IRepositoryBase<TEntity> where TEntity: class, IUidIdentity<TKey> where TKey: IEquatable<TKey>
{
    private readonly ImageSharingDbContext _context;

    protected BaseRepository(ImageSharingDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<TEntity>> GetByCriteria(ISpecification<TEntity> criteria)
    {
        return await GetByCriteriaQuery(criteria).ToListAsync();
    }
    
    public IQueryable<TEntity> GetByCriteriaQuery(ISpecification<TEntity> criteria)
    {
        return SpecificationToQueryEvaluator<TEntity>.ApplySpecification(
            _context.Set<TEntity>(),
            criteria
        );
    }

    public async Task<TEntity?> GetByGuid(Guid id)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Guid == id);
    }

    public async Task Add(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task Remove(TEntity entity)
    {
        await Task.FromResult(_context.Set<TEntity>().Remove(entity));
    }

}