using Application_Core.Model;
using Infrastructure.Manager;
using Infrastructure.Utility.Pagination;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Mapper;
using WebAPI.Response;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly AlbumManager _albumManager;

    public AlbumController(AlbumManager albumManager)
    {
        _albumManager = albumManager;
    }

    [HttpGet]
    [Route("/all")]
    public async Task<IActionResult> GetAll([FromQuery(Name = "page")] int page, [FromQuery(Name = "itemNumber")] int itemNumber = 5)
    {
        PaginatorResult<Album> paginator = await _albumManager.GetAllPaginated(itemNumber, page);
        PaginatorResult<AlbumResponse> response = paginator.MapToOtherType(AlbumMapper.FromAlbumToAlbumResponse);

        return Ok(response);
    }
}