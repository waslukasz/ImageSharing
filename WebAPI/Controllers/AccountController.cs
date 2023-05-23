using System.Net;
using Application_Core.Exception;
using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
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

        /// <summary>
        /// Gets current account
        /// </summary>
        /// <remarks>This can only be done by logged in user.</remarks>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task<IActionResult> GetCurrentAccount()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User) ?? throw new UserNotFoundException();
            var result = await _accountService.GetByNameAsync(currentUser.UserName);
            return Ok(result);
        }
        
        /// <summary>
        /// Gets account by username
        /// </summary>
        /// <remarks>This can only be done by logged in user.<br/>Required Admin rights to get other user account.</remarks>
        /// <param name="username">Username associated with account</param>
        /// <returns></returns>
        [HttpPost("Get/{username}")]
        public async Task<IActionResult> Get([FromRoute] string username)
        {
            if (!await HasAccessAsync(username)) return Unauthorized();
            return Ok(await _accountService.GetByNameAsync(username));
        }

        /// <summary>
        /// Create account
        /// </summary>
        /// <param name="request">New account object</param>
        /// <returns></returns>
        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
        {
            return await _accountService.CreateAsync(request) ? NoContent() : BadRequest();
        }
        
        /// <summary>
        /// Update account
        /// </summary>
        /// <remarks>This can only be done by logged in user.<br/>Required Admin rights to update other user account.</remarks>
        /// <param name="request">Updated account object</param>
        /// <param name="username">Username associated with account</param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        [HttpPatch("Update/{username}")]
        public async Task<IActionResult> Update([FromBody] UpdateAccountRequest request, [FromRoute] string username)
        {
            if (!await HasAccessAsync(username)) return Unauthorized();
            return await _accountService.UpdateAsync(username, request) ? NoContent() : throw new BadRequestException("Something did not work.", HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Delete account
        /// </summary>
        /// <remarks>This can only be done by logged in user.<br/>Required Admin rights to delete other user account.</remarks>
        /// <param name="username"></param>
        /// <returns></returns>
        // TODO: Nie można usunąć użytkownika, gdy ma dodane zdjęcie. Problem z bazą danych.
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
            var currentUser = await _userManager.GetUserAsync(HttpContext.User) ?? throw new UserNotFoundException();
            if (await _userManager.IsInRoleAsync(currentUser, "Admin")) return true;
            var user = await _userManager.FindByNameAsync(username) ?? throw new UserNotFoundException();
            return user == currentUser;
        }
    }
}
