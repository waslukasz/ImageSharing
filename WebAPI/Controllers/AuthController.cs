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
    [ApiController, Route("/api/auth")]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<UserEntity> _userManager;

        public AuthenticationController(IConfiguration configuration, IAuthService authService, UserManager<UserEntity> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }
        
        /// <summary>
        /// Get JWT Security token for account
        /// </summary>
        /// <param name="request">Account details</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            return Ok(new { token = await _authService.Login(request) });
        }

        /// <summary>
        /// Get current account roles
        /// </summary>
        /// <remarks>This can only be done by logged in user.</remarks>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        public async Task<GetRolesAccountResponse> GetRoles()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User) ?? throw new UserNotFoundException();
            var result = new GetRolesAccountResponse()
            {
                Username = currentUser.UserName,
                Roles = (await _userManager.GetRolesAsync(currentUser)).ToList()
            };
            return result;
        }
        
        /// <summary>
        /// Get account roles by username
        /// </summary>
        /// <remarks>Required Admin rights to get other account roles.</remarks>
        /// <returns></returns>
        [HttpGet("GetRoles/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<GetRolesAccountResponse> GetRolesByUser([FromRoute] string username)
        {
            var user = await _userManager.FindByNameAsync(username) ?? throw new UserNotFoundException();
            var result = new GetRolesAccountResponse()
            {
                Username = user.UserName,
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            };
            return result;
        }

        /// <summary>
        /// Assign role to account
        /// </summary>
        /// <remarks>Required Admin rights to assign role.</remarks>
        /// <returns></returns>
        /// <exception cref="BadRequestException">Account already has role.</exception>
        [HttpPost("AssignRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleAuthRequest request)
        {
            return await _authService.AssignRole(request) ? NoContent() : throw new BadRequestException("Account already in role.", HttpStatusCode.BadRequest);
        }
        
        /// <summary>
        /// Remove role from account
        /// </summary>
        /// <remarks>Required Admin rights to remove role.</remarks>
        /// <returns></returns>
        /// <exception cref="BadRequestException">Account did not had this role.</exception>
        [HttpPost("RemoveRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole([FromBody] RemoveRoleAuthRequest request)
        {
            return await _authService.RemoveRole(request) ? NoContent() : throw new BadRequestException("Account did not had this role.", HttpStatusCode.BadRequest);
        }
    }
}