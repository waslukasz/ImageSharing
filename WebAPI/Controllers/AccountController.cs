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
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterAccountRequest request)
        {
            return await _accountService.Register(request) ? NoContent() : BadRequest();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromForm] DeleteAccountRequest request)
        {
            return await _accountService.Delete(request) ? NoContent() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
        
        [HttpPut("ChangeUsername")]
        public async Task<IActionResult> ChangeUsername([FromForm] ChangeUsernameAccountRequest request)
        {
            return await _accountService.ChangeUsername(request) ? Ok() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
        
        [HttpPut("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail([FromForm] ChangeEmailAccountRequest request)
        {
            return await _accountService.ChangeEmail(request) ? Ok() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
        
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordAccountRequest request)
        {
            return await _accountService.ChangePassword(request) ? Ok() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
    }
}
