using Application_Core.Model;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repository.ImageRepository;

public class ImageRepository : BaseRepository<Image,int>, IImageRepository
{
    public ImageRepository(ImageSharingDbContext context) : base(context)
    {
        
    }

    public async Task<Image?> GetImageByGuid(Guid id)
    {
        return await Context.Images.Where(i => i.Guid == id).FirstOrDefaultAsync();
    }

    public async Task AddImage(Image image)
    {
        await Context.Images.AddAsync(image);
    }

    public async Task DeleteImage(Image image)
    {
        await Task.FromResult(Context.Images.Remove(image));
    }
}