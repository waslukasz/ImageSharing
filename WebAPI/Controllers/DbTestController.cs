using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.EF.Entity;
using Infrastructure.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Mapper;
using WebAPI.Request;
using WebAPI.Services;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DbTestController : ControllerBase
{
    private readonly ImageSharingDbContext _context;
    private readonly IImageService _imageService;

    public DbTestController(ImageSharingDbContext context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }
    
    [HttpGet("images")]
    public ActionResult<IEnumerable<Image>> GetImages()
    {
        return this._context.Images.Select(c => c).ToList();
    }
    
    [HttpPost("images/upload")]
    public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
    {
        UserEntity user = await GetFirstUser();
        
        await _imageService.CreateImage(ImageMapper.FromRequestToFileDto(request), user);
        
        return Ok();
    }

    [HttpPost("images/update")]
    public async Task<IActionResult> UpdateImage([FromForm] UpdateImageRequest request)
    {
        UserEntity user = await GetFirstUser();

        await _imageService.UpdateImage(ImageMapper.FromRequestToImageDto(request), user);
        
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteImage(UidRequest request)
    {
        UserEntity user = await GetFirstUser();

        await _imageService.DeleteImage(request.Id, user);
        
        return Ok();
    }

    // FOR TEST PURPOSE ONLY !
    private async Task<UserEntity> GetFirstUser()
    {
        return await _context.Users.FindAsync(1) ?? throw new NotFoundException();
    }
}