using Application_Core.Model;
using Infrastructure.FileManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    private readonly FileManager _fileManager;

    public ImageController(IImageService imageService, FileManager fileManager)
    {
        _imageService = imageService;
        _fileManager = fileManager;
    }

    [HttpGet]
    [Route("Download")]
    public async Task<IActionResult> DownloadImage([FromQuery] Guid id)
    {
        string contentType = "";
        Image image = await _imageService.GetImageWithStream(id);
        if (new FileExtensionContentTypeProvider().TryGetContentType(image.GetStoragePath(), out contentType))
        {
            return new FileStreamResult(image.Stream, contentType);
        }
        return BadRequest();
    }
    
    [HttpGet]
    [Route("DownloadThumbnail")]
    public async Task<IActionResult> DownloadThumbnail([FromQuery] Guid id)
    {
        string contentType = "";
        Image image = await _imageService.GetImageThumbnailWithStream(id);
        if (new FileExtensionContentTypeProvider().TryGetContentType(image.GetStoragePath(), out contentType))
        {
            return new FileStreamResult(image.Stream, contentType);
        }
        return BadRequest();
    }
}