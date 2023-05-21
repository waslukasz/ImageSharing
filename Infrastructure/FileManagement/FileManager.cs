using System.Drawing;
using ImageProcessor;
using ImageProcessor.Imaging;
using Infrastructure.FileManagement.FileSettings;
using LiteX.Storage.Core;

namespace Infrastructure.FileManagement;

public class FileManager
{
    public const string ThumbnailPrefix = "thumbnail-";
    
    private readonly ILiteXBlobServiceAsync _blobService; 
    
    public FileManager(ILiteXBlobServiceAsync blobService)
    {
        _blobService = blobService;
    }

    public async Task<bool> UploadImage(string blobName, Image image, BaseFileSettings settings)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            using (MemoryStream outstream = new MemoryStream())
            {
                using (ImageFactory factory = new ImageFactory())
                {
                    factory
                        .Load(image)
                        .Format(settings.GetFormat())
                        .Resize(new ResizeLayer(settings.GetSize(),ResizeMode.Max))
                        .Save(outstream);
                }

                return await _blobService.UploadBlobAsync(blobName,outstream);
            }
        }
    }
    
    public async Task<bool> DeleteFile(string blobName)
    {
        return await _blobService.DeleteBlobAsync(blobName);
    }
    
    public async Task<Stream> GetFileStream(string blobName)
    {
        return await _blobService.GetBlobAsync(blobName);
    }
    
    public static string GetThumbnailName(string filename)
    {
        return FileManager.ThumbnailPrefix + filename;
    }

}