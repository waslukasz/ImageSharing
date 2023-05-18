using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.Manager;
using Infrastructure.Utility.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Managers;
using WebAPI.Request;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostManager _postManager;
        public PostController(PostManager postManager)
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
