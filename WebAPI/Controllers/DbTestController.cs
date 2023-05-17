using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.EF.Entity;
using Infrastructure.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Mapper;
using WebAPI.Request;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DbTestController : ControllerBase
{
    private readonly ImageSharingDbContext _context;
    private readonly ImageManager _imageManager;
    
    public DbTestController(ImageSharingDbContext context, ImageManager imageManager)
    {
        _context = context;
        _imageManager = imageManager;
    }
    
    [HttpGet("images")]
    public ActionResult<IEnumerable<Image>> GetImages()
    {
        return this._context.Images.Select(c => c).ToList();
    }
    
    [HttpPost("images/upload")]
    public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
    {
        User user = await GetFirstUser();
        
        await _imageManager.CreateImage(ImageMapper.FromRequestToFileDto(request), user);
        
        return Ok();
    }

    [HttpPost("images/update")]
    public async Task<IActionResult> UpdateImage([FromForm] UpdateImageRequest request)
    {
        User user = await GetFirstUser();

        await _imageManager.UpdateImage(ImageMapper.FromRequestToImageDto(request), user);
        
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteImage(UidRequest request)
    {
        User user = await GetFirstUser();

        await _imageManager.DeleteImage(request.Id, user);
        
        return Ok();
    }

    // FOR TEST PURPOSE ONLY !
    private async Task<User> GetFirstUser()
    {
        return await _context.Users.FindAsync(1) ?? throw new NotFoundException();
    }
}