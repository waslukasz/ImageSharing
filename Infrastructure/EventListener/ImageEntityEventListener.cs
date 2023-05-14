using Application_Core.Model;
using Infrastructure.Database.FileManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.EventListener;

public class ImageEntityEventListener
{
    private readonly FileManager _fileManager;

    public ImageEntityEventListener(FileManager fileManager)
    {
        this._fileManager = fileManager;
    }
    public void OnImageDelete(object? sender, EntityStateChangedEventArgs entityStateChangedEventArgs)
    {
        if(!IsInstanceOfImage(entityStateChangedEventArgs.Entry.Entity))
            return;

        Image entity = (Image)entityStateChangedEventArgs.Entry.Entity;
            
        if (entityStateChangedEventArgs.Entry.State == EntityState.Deleted)
        {
            bool result = this._fileManager.DeleteFile(entity.GetStoragePath()).Result;

            Console.WriteLine("-------------------------------------------------");
            if(result == true)
                Console.WriteLine($"Deleting image - {entity.Title} - {entity.Guid} ");
            else
                Console.WriteLine($"An error occured while deleting image {entity.Title}");
            Console.WriteLine("-------------------------------------------------");
        }
    }

    public void OnImageCreate(object? sender, EntityTrackedEventArgs entityTrackedEventArgs)
    {
        if(!IsInstanceOfImage(entityTrackedEventArgs.Entry.Entity))
            return;
        
        Image entity = (Image)entityTrackedEventArgs.Entry.Entity;
        
        if (entityTrackedEventArgs.Entry.State == EntityState.Added)
        {
            
            bool result = this._fileManager.UploadFile(entity.GetStoragePath(),entity.Stream).Result;

            Console.WriteLine("-------------------------------------------------");
            if(result == true)
                Console.WriteLine($"File with name - {entity.Title} - successfully uploaded !");
            else
                Console.WriteLine($"An error occured while uploading image {entity.Title} ");
            Console.WriteLine("-------------------------------------------------");
        }
    }

    public void OnImageUpdate(object? sender, EntityStateChangedEventArgs entityStateChangedEventArgs)
    {
        throw new NotImplementedException();
    }

    private bool IsInstanceOfImage(object entity)
    {
        return entity is Image;
    }
    
}