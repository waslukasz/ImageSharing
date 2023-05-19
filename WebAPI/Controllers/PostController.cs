using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using WebAPI.Services;
using WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postManager;
        private readonly UserManager<UserEntity> _userManager;

        public PostController(IPostService postManager,
            UserManager<UserEntity> userManager)
        {
            _postManager = postManager;
            _userManager = userManager;
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
        
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] CreatePostRequest postDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user ==null)
                throw new UserNotFoundException();
            await _postManager.CreateAsync(postDto,user);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeletePostRequest postRequest)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user ==null)
                throw new UserNotFoundException();
            await _postManager.DeleteAsync(postRequest, user);
            return Ok();
        }
    }
}
