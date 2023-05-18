using System.Net;
using Application_Core.Exception;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterAccountRequest request)
        {
            return await _accountService.Register(request) ? NoContent() : BadRequest();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromForm] DeleteAccountRequest request)
        {
            return await _accountService.Delete(request) ? NoContent() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
        
        [HttpPut("changeUsername")]
        public async Task<IActionResult> ChangeUsername([FromForm] ChangeUsernameAccountRequest request)
        {
            return await _accountService.ChangeUsername(request) ? Ok() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
        
        [HttpPut("changeEmail")]
        public async Task<IActionResult> ChangeEmail([FromForm] ChangeEmailAccountRequest request)
        {
            return await _accountService.ChangeEmail(request) ? Ok() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
        
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordAccountRequest request)
        {
            return await _accountService.ChangePassword(request) ? Ok() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
    }
}
