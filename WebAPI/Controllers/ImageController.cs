using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.EF.Entity;
using LiteX.Storage.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly UserManager<UserEntity> _userManager;

    public ImageController(IImageService imageService, UserManager<UserEntity> userManager)
    {
        _imageService = imageService;
        _userManager = userManager;
    }

    /// <summary>
    /// Returns Image file
    /// </summary>
    /// <param name="id">Image Guid</param>
    /// <returns>FileStreamResult</returns>
    /// <exception cref="ImageNotFoundException">Throws only when image file specified in param as guid is not found on the server</exception>
    [HttpGet]
    [Route("Download")]
    public async Task<IActionResult> DownloadImage([FromQuery] Guid id)
    {
        string contentType = "";
        try
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);
            Image image = await _imageService.GetImageWithStream(id,user);
            if (new FileExtensionContentTypeProvider().TryGetContentType(image.GetStoragePath(), out contentType))
            {
                return new FileStreamResult(image.Stream!, contentType);
            }
            return BadRequest();
        }
        catch (StorageException e)
        {
            throw new ImageNotFoundException();
        }
        
    }
    
    /// <summary>
    /// Returns Thumbnail file
    /// </summary>
    /// <param name="id">Image 'NOT THUMBNAIL' Guid</param>
    /// <returns>FileStreamResult</returns>
    /// <exception cref="ImageNotFoundException">Throws only when thumbnail file specified in param as image guid is not found on the server</exception>
    [HttpGet]
    [Route("DownloadThumbnail")]
    public async Task<IActionResult> DownloadThumbnail([FromQuery] Guid id)
    {
        string contentType = "";
        try
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);
            Image image = await _imageService.GetImageThumbnailWithStream(id,user);
            if (new FileExtensionContentTypeProvider().TryGetContentType(image.GetStoragePath(), out contentType))
            {
                return new FileStreamResult(image.Stream!, contentType);
            }
            return BadRequest();
        }
        catch (StorageException e)
        {
            throw new ImageNotFoundException();
        }
    }
}