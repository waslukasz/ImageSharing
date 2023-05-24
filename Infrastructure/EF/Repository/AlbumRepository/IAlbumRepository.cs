using Application_Core.Common.Specification;
using Application_Core.Model;

namespace Infrastructure.EF.Repository.AlbumRepository;


public interface IAlbumRepository : IRepositoryBase<Album>
{
    public IQueryable<Album> GetAllQuery();

    public Task UpdateAlbum(Album album);
}