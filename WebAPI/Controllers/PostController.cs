using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using WebAPI.Services;
using WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using WebAPI.Mapper;
using WebAPI.Response;

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

            PaginatorResult<Post> data = await _postManager.GetAll(request.ItemNumber, request.Page);
            PaginatorResult<PostResponse> response = data.MapToOtherType(PostMapper.FromPostToPostResponse);
            
            response.Items = response.Items.Select(c =>
            {
                c.Thumbnail.DownloadUrl = this.Url.Action("DownloadThumbnail", "Image", new { Id = c.Id });
                c.Image.DownloadUrl = this.Url.Action("DownloadImage", "Image", new { Id = c.Id });
                return c;
            }).ToList();
            
            return Ok(response);
        }
        
        [HttpGet("getByUser")]
        public async Task<IActionResult> GetAllUserPost([FromQuery] GetUserPostRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            PaginatorResult<PostDto> data = await _postManager.GetUserPosts(request.Id,request.ItemNumber,request.Page);
            return Ok(data);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetByTags([FromQuery] SearchPostRequest request,[FromQuery] PaginationRequest paginationRequest)
        {
           var data=await _postManager.GetPostByTags(request,paginationRequest);
            return Ok(data);
        }
        
        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CreatePostRequest postDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user ==null)
                throw new UserNotFoundException();
            await _postManager.CreateAsync(postDto,user);
            return Ok();
        }

        [HttpPatch]
        [Route("Edit")]
        
        public async Task<IActionResult> Edit([FromForm] UpdatePostRequest postRequest)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user ==null)
                throw new UserNotFoundException();
            await _postManager.EditAsync(postRequest, user);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize]
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
