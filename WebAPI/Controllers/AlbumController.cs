using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Mapper;
using WebAPI.Request;
using WebAPI.Response;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumSerivce;
    private readonly UserManager<UserEntity> _userManager;

    public AlbumController(IAlbumService albumSerivce, UserManager<UserEntity> userManager)
    {
        _albumSerivce = albumSerivce;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        PaginatorResult<Album> paginator = await _albumSerivce.GetAllPaginated(paginationRequest.ItemNumber, paginationRequest.Page);
        PaginatorResult<AlbumResponse> response = paginator.MapToOtherType(AlbumMapper.FromAlbumToAlbumResponse);

        return Ok(response);
    }
    
    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Search([FromQuery] SearchAlbumRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        AlbumSearchDto param = request.ToParam();
        
        PaginatorResult<Album> paginator = await _albumSerivce.Search(param,request.Page,request.ItemNumber);
        PaginatorResult<AlbumResponse> response = paginator.MapToOtherType(AlbumMapper.FromAlbumToAlbumResponse);

        return Ok(response);
    }

    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetAlbum([FromQuery] UidRequest request)
    {
        Album album = await _albumSerivce.GetAlbum(request);
        AlbumWithImagesResponse response = AlbumMapper.FromAlbumToAlbumWithImagesResponse(album);
        
        response.Images = response.Images.Select(i =>
        {
            i.DownloadUrl = this.Url.Action("DownloadImage", "Image", new { Id = i.Id });
            return i;
        }).ToList();
        
        return Ok(response);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateAlbum([FromBody] CreateAlbumRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        UserEntity? userEntity = await _userManager.GetUserAsync(HttpContext.User);
        if (userEntity is null)
            return Unauthorized();

        Album album = await _albumSerivce.CreateAlbum(request,userEntity);
        AlbumWithImagesResponse response = AlbumMapper.FromAlbumToAlbumWithImagesResponse(album);
        response.Images = response.Images.Select(i =>
        {
            i.DownloadUrl = this.Url.Action("DownloadImage", "Image", new { Id = i.Id });
            return i;
        }).ToList();
        return Created(this.Url.Action("GetAlbum", new { Id = album.Guid }), response);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteAlbum([FromBody] UidRequest request)
    {
        Album album = await _albumSerivce.GetAlbum(request);
        UserEntity? currentUser = await _userManager.GetUserAsync(HttpContext.User);
        
        if (album.User != currentUser)
            return BadRequest();

        await _albumSerivce.DeleteAlbum(album);

        return NoContent();

    }
    
    [HttpPatch]
    [Route("update")]
    public async Task<IActionResult> EditAlbum([FromBody] UpdateAlbumRequest request, [FromQuery] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        UserEntity? currentUser = await _userManager.GetUserAsync(HttpContext.User);
        if (currentUser is null)
            return Unauthorized();
        
        Album updatedAlbum = await _albumSerivce.UpdateAlbum(request, currentUser, id);
        AlbumWithImagesResponse response = AlbumMapper.FromAlbumToAlbumWithImagesResponse(updatedAlbum);
        
        response.Images = response.Images.Select(i =>
        {
            i.DownloadUrl = this.Url.Action("DownloadImage", "Image", new { Id = i.Id });
            return i;
        }).ToList();
        
        return Ok(response);

    }
}
