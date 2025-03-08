using DOCOSoft.UserAPI.DTOs;
using DOCOSoft.UserAPI.Services;
using DOCOSoft.UserAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DOCOSoft.UserAPI.Controllers
{
    public class AuthController(IUserService userService, JwtService jwtService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await userService.GetUserByEmailAsync(request);

            if (user == null) return Unauthorized(new { message = "[MSG011] Invalid email or password." });

            if (user.Data != null)
            {
                var token = jwtService.GenerateToken(user.Data.Id.ToString(), user.Data.Email, user.Data.Role);
                return Ok(new { token });
            }
            else
            {
                return BadRequest( new { message = user.Message});
            }
        }
    }
}
