using DOCOSoft.UserAPI.DTOs;

namespace DOCOSoft.UserAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<string>> AuthenticateUserAsync(LoginRequestDto loginDto);
    }
}
