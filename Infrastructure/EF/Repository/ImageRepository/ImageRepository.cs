using Application_Core.Model;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repository.ImageRepository;

public class ImageRepository : IImageRepository
{
    private readonly ImageSharingDbContext _context;

    public ImageRepository(ImageSharingDbContext context)
    {
        _context = context;
    }

    public async Task<Image?> GetImageByGuid(Guid id)
    {
        return await _context.Images.Where(i => i.Guid == id).FirstOrDefaultAsync();
    }

    public async Task AddImage(Image image)
    {
        await _context.Images.AddAsync(image);
    }

    public async Task DeleteImage(Image image)
    {
        await Task.FromResult(_context.Images.Remove(image));
    }
}