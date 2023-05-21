using Application_Core.Common.Specification;

namespace Infrastructure.EF.Repository;

public interface IRepositoryBase<TEntity>
{
    Task<IEnumerable<TEntity>> GetByCriteria(ISpecification<TEntity> criteria);
    IQueryable<TEntity> GetByCriteriaQuery(ISpecification<TEntity> criteria);
    Task<TEntity?> GetByGuid(Guid id);
    Task Add(TEntity entity);
    Task Remove(TEntity entity);
}