﻿using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.Utility;
using LiteX.Storage.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Manager;

public class ImageManager
{
    public readonly ILiteXBlobService _BlobService;

    public readonly ImageSharingDbContext _Context;

    public readonly UniqueFileNameAssigner _NameAssigner;

    public ImageManager(ILiteXBlobService blobService, ImageSharingDbContext context, UniqueFileNameAssigner nameAssigner)
    {
        _BlobService = blobService;
        _Context = context;
        _NameAssigner = nameAssigner;
    }

    public async Task CreateImage(FileDto imageDto, UserEntity user)
    {
        Image image = imageDto.ToImage(_NameAssigner);
        image.User = user;

        _Context.Add(image);
        _Context.SaveChanges();
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

    public async Task<IEnumerable<Image>> GetAll()
    {
        return await _Context.Images.ToListAsync();
    }
    
}