using System.Drawing;
using Application_Core.Model;
using Infrastructure.FileManagement;
using Infrastructure.FileManagement.FileSettings;
using Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Image = Application_Core.Model.Image;
using SystemImage = System.Drawing.Image;

namespace Infrastructure.EventListener;

public class ImageEntityEventListener
{
    private readonly FileManager _fileManager;
    private readonly UniqueFileNameAssigner _nameAssigner;

    public ImageEntityEventListener(FileManager fileManager, UniqueFileNameAssigner nameAssigner)
    {
        this._fileManager = fileManager;
        _nameAssigner = nameAssigner;
    }
    public void OnImageDelete(object? sender, EntityEntryEventArgs args)
    {
        if(args.Entry.Entity is not Image entity)
            return;

        if (args.Entry.State != EntityState.Deleted)
            return;
        
        _fileManager.DeleteFile(entity.GetStoragePath()).Wait();
        _fileManager.DeleteFile(FileManager.GetThumbnailName(entity.GetStoragePath())).Wait();

    }

#pragma warning disable CA1416
    public void OnImageCreate(object? sender, EntityEntryEventArgs args)
    {
        if(args.Entry.Entity is not Image entity)
            return;

        if (args.Entry.State != EntityState.Added)
            return;
        
        if(entity.Stream is null)
            return;

        string thumbnailName = FileManager.GetThumbnailName(entity.GetStoragePath());

        SystemImage image = SystemImage.FromStream(entity.Stream);
        SystemImage thumbnail = SystemImage.FromStream(entity.Stream);
  
        _fileManager.UploadImage(
            entity.GetStoragePath(),
            image,
            new ImageFileSettings(new Size(image.Width,image.Height))
        ).Wait();

        _fileManager.UploadImage(
            thumbnailName,
            thumbnail,
            new ThumbnailFileSettings()
            ).Wait();
        

        entity.Thumbnail = new Thumbnail()
        {
            Name = thumbnailName
        };

    }

    public void OnImageUpdate(object? sender, EntityEntryEventArgs args)
    {
        if(args.Entry.Entity is not Image entity)
            return;

        if (args.Entry.State != EntityState.Modified)
            return;

        Image imageBeforeModification = (Image)args.Entry.OriginalValues.ToObject();

        if(entity.Stream is null)
            return;
        
        string oldThumbnailName = FileManager.GetThumbnailName(imageBeforeModification.GetStoragePath());
        this._fileManager.DeleteFile(imageBeforeModification.GetStoragePath()).Wait();
        this._fileManager.DeleteFile(oldThumbnailName).Wait();
        
        string thumbnailName = FileManager.GetThumbnailName(imageBeforeModification.GetStoragePath());
        
        SystemImage image = SystemImage.FromStream(entity.Stream);
        SystemImage thumbnail = SystemImage.FromStream(entity.Stream);
        
        this._fileManager.UploadImage(
            entity.GetStoragePath(),
            image,
            new ImageFileSettings(new Size(image.Width,image.Height))
        ).Wait();
        
        this._fileManager.UploadImage(
            thumbnailName,
            thumbnail,
            new ThumbnailFileSettings()
            ).Wait();
    }
#pragma warning restore CA1416     

}