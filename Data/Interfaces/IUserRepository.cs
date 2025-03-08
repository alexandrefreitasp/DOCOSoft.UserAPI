using DOCOSoft.UserAPI.DTOs;
using DOCOSoft.UserAPI.Models;

namespace DOCOSoft.UserAPI.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseDto<User>?> GetUserByIdAsync(int id);
        Task<ResponseDto<User>?> GetUserByEmailAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto<User>> AddUserAsync(RequestCreateDto requestCreateDto);
        Task<ResponseDto<User>> UpdateUserAsync(int id ,RequestUpdateDto requestUpdateDto);
    }
}
