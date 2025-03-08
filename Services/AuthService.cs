using DOCOSoft.UserAPI.Data.Interfaces;
using DOCOSoft.UserAPI.DTOs;
using DOCOSoft.UserAPI.Services.Interfaces;

namespace DOCOSoft.UserAPI.Services
{
    public class AuthService(IUserRepository repository, IJwtService jwtService) : IAuthService
    {
        public async Task<ResponseDto<string>> AuthenticateUserAsync(LoginRequestDto loginDto)
        {
            var user = await repository.GetByEmailAsync(loginDto.Email);
            if (user == null)
                return new ResponseDto<string>(null, "[MSG003] User not found.");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return new ResponseDto<string>(null, "[MSG013] Invalid email or password.");

            var token = jwtService.GenerateToken(user.Id.ToString(), user.Email);
            return new ResponseDto<string>(token);
        }
    }
}
