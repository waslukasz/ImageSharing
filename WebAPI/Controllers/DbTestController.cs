using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.Service;
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
    private readonly ImageService _imageService;
    
    public DbTestController(ImageSharingDbContext context, ImageService imageService)
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
        await _imageService.CreateImage(ImageMapper.FromRequestToFileDto(request));
        return Ok();
    }

    [HttpPost("images/update")]
    public async Task<IActionResult> UpdateImage([FromForm] UpdateImageRequest request)
    {
        Image? entity = this._context.Images.Where(i => i.Guid == request.Id).Include(i => i.Post).FirstOrDefault();
        if (entity is null)
            return NotFound();

        // IMPORTANT !
        // This line makes sure the entity state always changes to 'MODIFIED',
        // so event listener can catch this and update file correctly.
        entity.Guid = Guid.NewGuid();
        
        entity.Stream = request.File.OpenReadStream();
        entity.Title = request.Title;

        await this._context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public IActionResult DeleteImage(UidRequest request)
    {
        Image? entity = this._context.Images.Where(i => i.Guid == request.Id).Include(i => i.Post).FirstOrDefault();
        if (entity is null)
            return NotFound();
        
        if(entity.Post is not null)
            this._context.Posts.Remove(entity.Post);
        
        this._context.Images.Remove(entity);
        this._context.SaveChanges();
        return Ok();
    }
}