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
    private readonly ILiteXBlobService _blobService;
    private readonly ImageSharingDbContext _context;
    private readonly UniqueFileNameAssigner _nameAssigner;

    public ImageService(ILiteXBlobService blobService, ImageSharingDbContext context, UniqueFileNameAssigner nameAssigner)
    {
        _blobService = blobService;
        _context = context;
        _nameAssigner = nameAssigner;
    }

    public async Task CreateImage(FileDto imageDto, UserEntity user)
    {
        Image image = imageDto.ToImage(_nameAssigner);
        image.User = user;

        _context.Add(image);
        _context.SaveChanges();
    }

    public async Task UpdateImage(ImageDto imageDto, UserEntity user)
    {
        Image? entity = await _context.Images.FindAsync(imageDto.Guid,user);
        if (entity is null)
            throw new ImageNotFoundException();

        Image updatedImage = imageDto.ToImage(_nameAssigner);
        
        entity.Stream = updatedImage.Stream;
        entity.Guid = Guid.NewGuid();
        entity.Title = updatedImage.Title;
        entity.Slug = updatedImage.Slug;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteImage(Guid id, UserEntity user)
    {
        Image? entity = await _context.Images.Where(i => i.User == user && i.Guid == id).FirstOrDefaultAsync();
        if (entity is null)
            throw new ImageNotFoundException();

        _context.Images.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Image>> GetAll()
    {
        return await _context.Images.ToListAsync();
    }
}