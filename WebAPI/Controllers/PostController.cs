﻿using Application_Core.Model;
using Infrastructure.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostManager _postManager;
        public PostController(PostManager postManager)
        {
            _postManager = postManager;
        }
        [HttpGet]
        [Route("/AllPosts")]
        public async Task<IActionResult> GetAll([FromQuery] int maxItem, [FromQuery] int page)
        {

            var data = await _postManager.GetAll(maxItem, page);
            return Ok(data);
        }
        [HttpGet]
        [Route("/GetAllUserPost")]
        public async Task<IActionResult> GetAllUserPost([FromQuery] int userId
            ,[FromQuery] int maxItems, [FromQuery] int page)
        {
            var data = await _postManager.GetUserPosts(maxItems,page,userId);
            return Ok(data);
        }
        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> Create([FromForm] Post post)
        {
            return Ok();
        }
    }
}
