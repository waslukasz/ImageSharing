using Application_Core.Common.Identity;
using Application_Core.Common.Specification;

namespace Application_Core.Common.Repository;

public interface IGenericRepository<T,K> where T: IUidIdentity<K> where K: IEquatable<K>
{
    Task<T?> FindByIdAsync(K id);

    Task<T?> FindByGuidAsync(Guid id);
    
    Task<List<T>> FindAllAsync();

    T? FindById(K id);
    
    T? FindByGuid(Guid id);

    List<T> FindAll();
    
    T Add(T o);
    
    void RemoveById(K id);
    
    void RemoveByGuid(Guid id);
    
    void Update(K id, T o);
    
    void Update(Guid id, T o);
    
    IEnumerable<T> FindBySpecification(ISpecification<T> specification = null);
}