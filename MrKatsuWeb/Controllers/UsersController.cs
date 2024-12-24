using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrKatsuWeb.Application.Interfaces.System;
using MrKatsuWeb.DTO.Users;
using MrKatsuWeb.Utilities;

namespace MrKatsuWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromForm] LoginRequest request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var token = await _service.Authenticate(request);
            if (string.IsNullOrEmpty(token))
            {
                return APIRespone.Error("Invalid username or password");
            }
            return APIRespone.Success(token);
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.Register(request);
            if (!result)
                return APIRespone.Error("Đăng kí không thành công");
            return APIRespone.Success("Đăng kí thành công");
        }
    }
}
