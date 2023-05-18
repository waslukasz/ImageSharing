using Application_Core.Common.Specification;
using Application_Core.Model;

namespace Infrastructure.EF.Repository.AlbumRepository;


public interface IAlbumRepository
{
    public Task<Album?> GetAlbumByGuid(Guid id);

    public Task<IEnumerable<Album>> GetAlbumsByCriteria(ISpecification<Album> criteria);
    
    public IQueryable<Album> GetAlbumsByCriteriaQuery(ISpecification<Album> criteria);

    public IQueryable<Album> GetAllQuery();
    public Task RemoveAlbum(Album album);

    public Task UpdateAlbum(Album album, string title, string description);
}