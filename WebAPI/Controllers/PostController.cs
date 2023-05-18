using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Pagination;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postManager;
        public PostController(IPostService postManager)
        {
            _postManager = postManager;
        }
        
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            PaginatorResult<PostDto> data = await _postManager.GetAll(request.ItemNumber, request.Page);
            return Ok(data);
        }
        
        [HttpGet("getByUser")]
        public async Task<IActionResult> GetAllUserPost([FromQuery] GetUserPostRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            PaginatorResult<PostDto> data = await _postManager.GetUserPosts(request.Id,request.ItemNumber,request.Page);
            return Ok(data);
        }
        
        //TODO: dokończyć metodę
        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> Create([FromForm] Post post)
        {
            return Ok();
        }
    }
}
