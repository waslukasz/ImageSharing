using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Repository.ImageRepository;
using Infrastructure.FileManagement;
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

    private readonly IImageRepository _imageRepository;

    private readonly FileManager _fileManager;

    public ImageService(ILiteXBlobService blobService, ImageSharingDbContext context, UniqueFileNameAssigner nameAssigner, IImageRepository imageRepository, FileManager fileManager)
    {
        _blobService = blobService;
        _context = context;
        _nameAssigner = nameAssigner;
        _imageRepository = imageRepository;
        _fileManager = fileManager;
    }

    public async Task<Image> GetImageWithStream(Guid id)
    {
        Image image = await _imageRepository.GetByGuid(id) ?? throw new ImageNotFoundException();
        image.Stream = await _fileManager.GetFileStream(image.GetStoragePath());

        return image;

    }
    
    public async Task<Image> GetImageThumbnailWithStream(Guid id)
    {
        Image image = await _imageRepository.GetByGuid(id) ?? throw new ImageNotFoundException();
        image.Stream = await _fileManager.GetFileStream(FileManager.GetThumbnailName(image.GetStoragePath()));

        return image;

    }

    public async Task<Image> CreateImage(FileDto imageDto, UserEntity user)
    {
        Image image = imageDto.ToImage(_nameAssigner);
        image.User = user;
        
        var result = _context.Add(image);
        _context.SaveChanges();
        return result.Entity;
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
}