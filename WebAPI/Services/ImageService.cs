using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.Database.Seed.Generator;
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
    private readonly ImageSharingDbContext _context;

    private readonly UniqueFileNameAssigner _nameAssigner;

    private readonly IImageRepository _imageRepository;

    private readonly FileManager _fileManager;

    public ImageService(ImageSharingDbContext context, UniqueFileNameAssigner nameAssigner, IImageRepository imageRepository, FileManager fileManager)
    {
        _context = context;
        _nameAssigner = nameAssigner;
        _imageRepository = imageRepository;
        _fileManager = fileManager;
    }

    public async Task<Image> GetImageWithStream(Guid id, UserEntity? user)
    {
        Image image = await GetImageOrThrowWhenHidden(id, user);
        image.Stream = await _fileManager.GetFileStream(image.GetStoragePath());

        return image;

    }
    
    public async Task<Image> GetImageThumbnailWithStream(Guid id, UserEntity? user)
    {
        Image image = await GetImageOrThrowWhenHidden(id, user);
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

    public async Task UpdateImage(ImageDto imageDto)
    {
        Image? entity = await _imageRepository.GetByGuid(imageDto.Guid);
        if (entity is null)
            throw new ImageNotFoundException();
        
        Image updatedImage = imageDto.ToImage(_nameAssigner);
        
        entity.Stream = updatedImage.Stream;
        entity.Guid = imageDto.Guid;
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

    private async Task<Image> GetImageOrThrowWhenHidden(Guid id, UserEntity? user)
    {
        BaseSpecification<Image> criteria = new BaseSpecification<Image>();
        criteria
            .AddCriteria(i => i.Guid == id)
            .AddInclude(i => i.Post)
            .AddInclude(i => i.Post.Status)
            .AddInclude(i => i.User);

        Image? image = await _imageRepository.GetByCriteriaSingle(criteria) ?? throw new ImageNotFoundException();

        if (image.Post.Status.Name == StatusEnum.Hidden.ToString() && image.Post.User != user)
        {
            throw new ImageNotFoundException("Content unavailable.");
        }

        return image;
    }
}