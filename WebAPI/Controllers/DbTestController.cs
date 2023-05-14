using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.Service;
using LiteX.Storage.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
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