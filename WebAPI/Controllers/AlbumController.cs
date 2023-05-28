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
    
    /// <summary>
    /// Returns all Albums
    /// </summary>
    /// <param name="paginationRequest">Pagination object describing current page and max item per page</param>
    /// <returns>PaginatorResult</returns>
    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        PaginatorResult<Album> paginator = await _albumSerivce.GetAllPaginated(paginationRequest.ItemNumber, paginationRequest.Page);
        PaginatorResult<AlbumResponse> response = paginator.MapToOtherType(AlbumMapper.FromAlbumToAlbumResponse);

        return Ok(response);
    }
    
    /// <summary>
    /// Returns list of Albums matching given criteria
    /// </summary>
    /// <param name="request">Request containing criteria and pagination fields</param>
    /// <returns>PaginatorResult</returns>
    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> Search([FromQuery] SearchAlbumRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        AlbumSearchDto param = request.ToParam();
        
        PaginatorResult<Album> paginator = await _albumSerivce.Search(param,request.Page,request.ItemNumber);
        PaginatorResult<AlbumResponse> response = paginator.MapToOtherType(AlbumMapper.FromAlbumToAlbumResponse);

        return Ok(response);
    }

    /// <summary>
    /// Returns album matching given Guid
    /// </summary>
    /// <param name="id">Album Guid</param>
    /// <returns>AlbumWithImageResponse</returns>
    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetAlbum(Guid id)
    {
        Album album = await _albumSerivce.GetAlbum(id);
        AlbumWithImagesResponse response = AlbumMapper.FromAlbumToAlbumWithImagesResponse(album);
        
        response.Images = response.Images.Select(i =>
        {
            i.DownloadUrl = this.Url.Action("DownloadImage", "Image", new { Id = i.Id });
            return i;
        }).ToList();
        
        return Ok(response);
    }

    /// <summary>
    /// Creates Album by requested data
    /// </summary>
    /// <param name="request">Object containing required fields to create Album</param>
    /// <returns>Created Album</returns>
    [HttpPost]
    [Route("Create")]
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

    /// <summary>
    /// Deletes Album 
    /// </summary>
    /// <param name="id">Album Guid</param>
    /// <returns>Bad request when specified album is not a currents user album, otherwise NoContent</returns>
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteAlbum(Guid id)
    {
        Album album = await _albumSerivce.GetAlbum(id);
        UserEntity? currentUser = await _userManager.GetUserAsync(HttpContext.User);
        
        if (album.User != currentUser)
            return BadRequest();

        await _albumSerivce.DeleteAlbum(album);

        return NoContent();

    }
    
    /// <summary>
    /// Updates Album by fields specified in request body
    /// </summary>
    /// <param name="request">Object containing fields to update</param>
    /// <param name="id">Album Guid</param>
    /// <returns>Bad request when requested params fail validation, otherwise updated Album</returns>
    [HttpPatch]
    [Route("update/{id}")]
    public async Task<IActionResult> EditAlbum([FromBody] UpdateAlbumRequest request, Guid id)
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
