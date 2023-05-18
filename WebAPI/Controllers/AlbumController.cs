using Application_Core.Model;
using Infrastructure.Manager;
using Infrastructure.Manager.Param;
using Infrastructure.Utility.Pagination;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Mapper;
using WebAPI.Request;
using WebAPI.Response;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public AlbumController(IAlbumService albumService)
    {
        _albumService = albumService;
    }

    [HttpGet]
    [Route("/all")]
    public async Task<IActionResult> GetAll(PaginationRequest paginationRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();
            
        PaginatorResult<Album> paginator = await _albumService.GetAllPaginated(paginationRequest.itemNumber, paginationRequest.page);
        PaginatorResult<AlbumResponse> response = paginator.MapToOtherType(AlbumMapper.FromAlbumToAlbumResponse);

        return Ok(response);
    }
    
    [HttpGet]
    [Route("/search")]
    public async Task<IActionResult> Search(SearchAlbumRequest request, PaginationRequest paginationRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        AlbumSearchDto param = request.ToParam();
        
        PaginatorResult<Album> paginator = await _albumService.Search(param,paginationRequest.page,paginationRequest.itemNumber);
        PaginatorResult<AlbumResponse> response = paginator.MapToOtherType(AlbumMapper.FromAlbumToAlbumResponse);

        return Ok(response);
    }
}