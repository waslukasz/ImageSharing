using System.Net;
using Application_Core.Exception;
using Application_Core.Model;
using Application_Core.Model.Interface;
using Infrastructure.Database;
using Infrastructure.Database.Seed.Generator;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.Utility;
using LiteX.Storage.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class ImageService
{
    public readonly ILiteXBlobService _BlobService;

    public readonly ImageSharingDbContext _Context;

    public readonly UniqueFileNameAssigner _NameAssigner;

    public ImageService(ILiteXBlobService blobService, ImageSharingDbContext context, UniqueFileNameAssigner nameAssigner)
    {
        _BlobService = blobService;
        _Context = context;
        _NameAssigner = nameAssigner;
    }

    public async Task CreateImage(FileDto imageDto)
    {
        //for test purposes only !
        IUser<int>? user = await _Context.Users.FindAsync(1);
        if (user is null)
            throw new NotFoundException("User not found !");
        //-----------------------
        
        Image image = imageDto.ToImage();
        image.User = user;
        image.Slug = _NameAssigner.RenameFile(imageDto);

        _Context.Add(image);
        _Context.SaveChanges();
    }

    public async Task<IEnumerable<Image>> GetAll()
    {
        return await _Context.Images.ToListAsync();
    }
    
}