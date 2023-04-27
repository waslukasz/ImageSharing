namespace Application_Core.Common.Repository;

public interface IGenericUidRepository<T,K> : IGenericRepository<T,K> where T: IIdentity<K> where K: IComparable<K>
{
    Task<K?> FindByGuidAsync(Guid guid);
    
    K? FindByGuid(Guid guid);

    void RemoveByGuid(Guid guid);

}