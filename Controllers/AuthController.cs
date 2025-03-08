using DOCOSoft.UserAPI.DTOs;
using DOCOSoft.UserAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DOCOSoft.UserAPI.Controllers
{
    
        [ApiController]
        [Route("api/auth")]
        public class AuthController(IAuthService authService) : ControllerBase
        {
            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
            {
                var result = await authService.AuthenticateUserAsync(loginDto);

                if (result.Data == null)
                    return Unauthorized(new { message = result.Message });

                return Ok(new { Token = result.Data });
            }
        }
 }
