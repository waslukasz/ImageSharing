using Application_Core.Model;
using Infrastructure.FileManagement;
using Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        
        bool result = this._fileManager.DeleteFile(entity.GetStoragePath()).Result;

        Console.WriteLine("-------------------------------------------------");
        if(result == true)
            Console.WriteLine($"Deleting image - {entity.Title} - {entity.Guid} ");
        else
            Console.WriteLine($"An error occured while deleting image {entity.Title}");
        Console.WriteLine("-------------------------------------------------");
    }

    public void OnImageCreate(object? sender, EntityEntryEventArgs args)
    {
        if(args.Entry.Entity is not Image entity)
            return;

        if (args.Entry.State != EntityState.Added)
            return;
        
        bool result = this._fileManager.UploadFile(entity.GetStoragePath(),entity.Stream).Result;

        Console.WriteLine("-------------------------------------------------");
        if(result == true)
            Console.WriteLine($"File with name - {entity.Title} - successfully uploaded !");
        else
            Console.WriteLine($"An error occured while uploading image {entity.Title} ");
        Console.WriteLine("-------------------------------------------------");
        
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

        this._fileManager.DeleteFile(imageBeforeModification.GetStoragePath()).Wait();
        this._fileManager.UploadFile(entity.GetStoragePath(), entity.Stream).Wait();
        
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"File with name - {entity.Title} - successfully updated !");
        Console.WriteLine("-------------------------------------------------");
    }
    
}