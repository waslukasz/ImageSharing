using Application_Core.Model;
using Infrastructure.Manager;
using Infrastructure.Manager.Param;
using Infrastructure.Utility.Pagination;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Managers;
using WebAPI.Mapper;
using WebAPI.Request;
using WebAPI.Response;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly AlbumManager _albumManager;

    public AlbumController(AlbumManager albumManager)
    {
        _albumManager = albumManager;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        PaginatorResult<Album> paginator = await _albumManager.GetAllPaginated(paginationRequest.ItemNumber, paginationRequest.Page);
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
        
        PaginatorResult<Album> paginator = await _albumManager.Search(param,request.Page,request.ItemNumber);
        PaginatorResult<AlbumResponse> response = paginator.MapToOtherType(AlbumMapper.FromAlbumToAlbumResponse);

        return Ok(response);
    }
}