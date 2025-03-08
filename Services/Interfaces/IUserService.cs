using DOCOSoft.UserAPI.DTOs;
using DOCOSoft.UserAPI.Models;

namespace DOCOSoft.UserAPI.Services.Interfaces
{
    public interface IUserService
    {

        Task<ResponseDto<User>?> GetUserByIdAsync(int id);
        Task<ResponseDto<User>?> GetUserByEmailAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto<User>> AddUserAsync(RequestCreateDto requestCreateDto);
        Task<ResponseDto<User>?> UpdateUserAsync(int id, RequestUpdateDto requestUpdateDto);


    }
}
