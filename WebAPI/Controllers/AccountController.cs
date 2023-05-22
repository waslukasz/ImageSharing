using System.Net;
using Application_Core.Exception;
using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using WebAPI.Response;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<UserEntity> _userManager;

        public AccountController(IAccountService accountService, UserManager<UserEntity> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpPost("Get/{username}")]
        public async Task<IActionResult> Get([FromRoute] string username)
        {
            if (!await HasAccessAsync(username)) return Unauthorized();
            return Ok(await _accountService.GetByNameAsync(username));
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] RegisterAccountRequest request)
        {
            return await _accountService.CreateAsync(request) ? NoContent() : BadRequest();
        }
        
        [HttpPatch("Update/{username}")]
        public async Task<IActionResult> Update([FromBody] UpdateAccountRequest request, [FromRoute] string username)
        {
            if (!await HasAccessAsync(username)) return Unauthorized();
            return await _accountService.UpdateAsync(username, request) ? NoContent() : throw new BadRequestException("Something did not work.", HttpStatusCode.BadRequest);
        }

        [HttpDelete("Delete/{username}")]
        public async Task<IActionResult> Delete([FromRoute] string username)
        {
            if (!await HasAccessAsync(username)) return Unauthorized();
            var user = await _userManager.FindByNameAsync(username);
            await _accountService.DeleteAsync(user);
            return NoContent();
        }

        private async Task<bool> HasAccessAsync(string username)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (await _userManager.IsInRoleAsync(currentUser, "Admin")) return true;
            var user = await _userManager.FindByNameAsync(username) ?? throw new UserNotFoundException();
            return user == currentUser ? true : false;
        }
    }
}
