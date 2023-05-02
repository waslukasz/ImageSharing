using Application_Core.Common.Specification;

namespace Application_Core.Common.Repository;

public interface IGenericRepository<T,K> where T: IIdentity<K> where K: IEquatable<K>
{
    Task<T?> FindByIdAsync(K id);
    
    Task<List<T>> FindAllAsync();

    T? FindById(K id);

    List<T> FindAll();
    
    T Add(T o);
    
    void RemoveById(K id);
    
    void Update(K id, T o);
    
    IEnumerable<T> FindBySpecification(ISpecification<T> specification = null);
}