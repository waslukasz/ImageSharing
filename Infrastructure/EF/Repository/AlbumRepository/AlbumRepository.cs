using System.Data.Entity;
using Application_Core.Common.Specification;
using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.EF.Evaluator;

namespace Infrastructure.EF.Repository.AlbumRepository;

public class AlbumRepository : IAlbumRepository
{
    private readonly ImageSharingDbContext _context;

    public AlbumRepository(ImageSharingDbContext context)
    {
        _context = context;
    }

    public async Task<Album?> GetAlbumByGuid(Guid id)
    {
        return await _context.Albums.FindAsync(id);
    }

    public async Task<List<Album>> GetAlbumsByCriteria(ISpecification<Album> criteria)
    {
        return await SpecificationToQueryEvaluator<Album>.ApplySpecification(
            _context.Albums,
            criteria
        ).ToListAsync();
    }

    public IQueryable<Album> GetAlbumsByCriteriaQuery(ISpecification<Album> criteria)
    {
        return SpecificationToQueryEvaluator<Album>.ApplySpecification(
            _context.Albums,
            criteria
        );
    }

    public IQueryable<Album> GetAllQuery()
    {
        return _context.Albums;
    }

    public async Task RemoveAlbum(Album album)
    {
        _context.Albums.Remove(album);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAlbum(Album album, string title, string description)
    { 
        album.Title = title;
        album.Description = description;
        await _context.SaveChangesAsync();
    }
}