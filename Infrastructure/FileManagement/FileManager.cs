using LiteX.Storage.Core;

namespace Infrastructure.FileManagement;

public class FileManager
{
    private readonly ILiteXBlobServiceAsync _blobService; 
    
    public FileManager(ILiteXBlobServiceAsync blobService)
    {
        _blobService = blobService;
    }

    public async Task<bool> DeleteFile(string blobName)
    {
        return await _blobService.DeleteBlobAsync(blobName);
    }

    public async Task<bool> UploadFile(string blobName, Stream fileStream)
    {
        return await _blobService.UploadBlobAsync(blobName, fileStream);
    }
}