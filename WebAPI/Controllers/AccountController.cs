using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application_Core.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Managers;
using WebAPI.Managers.Interfaces;
using WebAPI.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterAccountRequest request)
        {
            return await _accountManager.Register(request) ? NoContent() : BadRequest();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromForm] DeleteAccountRequest request)
        {
            return await _accountManager.Delete(request) ? NoContent() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
        
        [HttpPut("changeUsername")]
        public async Task<IActionResult> ChangeUsername([FromForm] ChangeUsernameAccountRequest request)
        {
            return await _accountManager.ChangeUsername(request) ? Ok() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
        
        [HttpPut("changeEmail")]
        public async Task<IActionResult> ChangeEmail([FromForm] ChangeEmailAccountRequest request)
        {
            return await _accountManager.ChangeEmail(request) ? Ok() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
        
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordAccountRequest request)
        {
            return await _accountManager.ChangePassword(request) ? Ok() : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
        }
    }
}
