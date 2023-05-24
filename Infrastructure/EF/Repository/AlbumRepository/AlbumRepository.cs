using System.Data.Entity;
using Application_Core.Common.Specification;
using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.EF.Evaluator;

namespace Infrastructure.EF.Repository.AlbumRepository;

public class AlbumRepository : BaseRepository<Album,int>, IAlbumRepository
{
    public AlbumRepository(ImageSharingDbContext context) : base(context)
    {
        
    }

    public IQueryable<Album> GetAllQuery()
    {
        return Context.Albums;
    }

    public async Task UpdateAlbum(Album album)
    {
        Context.Albums.Update(album);
        await Context.SaveChangesAsync();
    }
}