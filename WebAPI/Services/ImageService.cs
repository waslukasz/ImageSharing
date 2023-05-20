using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.Utility;
using LiteX.Storage.Core;
using Microsoft.EntityFrameworkCore;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services;

public class ImageService : IImageService
{
    private readonly ILiteXBlobService _BlobService;

    private readonly ImageSharingDbContext _Context;

    private readonly UniqueFileNameAssigner _NameAssigner;

    public ImageService(ILiteXBlobService blobService, ImageSharingDbContext context, UniqueFileNameAssigner nameAssigner)
    {
        _BlobService = blobService;
        _Context = context;
        _NameAssigner = nameAssigner;
    }

    public async Task<Image> CreateImage(FileDto imageDto, UserEntity user)
    {
        Image image = imageDto.ToImage(_NameAssigner);
        image.User = user;
        
        var result = _Context.Add(image);
        _Context.SaveChanges();
        return result.Entity;
    }

    public async Task UpdateImage(ImageDto imageDto, UserEntity user)
    {
        Image? entity = await _Context.Images.FindAsync(imageDto.Guid,user);
        if (entity is null)
            throw new ImageNotFoundException();

        Image updatedImage = imageDto.ToImage(_NameAssigner);
        
        entity.Stream = updatedImage.Stream;
        entity.Guid = Guid.NewGuid();
        entity.Title = updatedImage.Title;
        entity.Slug = updatedImage.Slug;

        await _Context.SaveChangesAsync();

    }

    public async Task DeleteImage(Guid id, UserEntity user)
    {
        Image? entity = await _Context.Images.Where(i => i.User == user && i.Guid == id).FirstOrDefaultAsync();
        if (entity is null)
            throw new ImageNotFoundException();

        _Context.Images.Remove(entity);
        await _Context.SaveChangesAsync();
    }

    public async Task<Image?> ImageFindByGuid(Guid id)
        => await _Context.Images.Where(x => x.Guid == id).FirstOrDefaultAsync();
    public async Task<IEnumerable<Image>> GetAll()
    {
        return await _Context.Images.ToListAsync();
    }
    
}