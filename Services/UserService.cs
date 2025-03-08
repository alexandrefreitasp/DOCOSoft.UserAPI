using DOCOSoft.UserAPI.Data.Interfaces;
using DOCOSoft.UserAPI.DTOs;
using DOCOSoft.UserAPI.Models;
using DOCOSoft.UserAPI.Services.Interfaces;

namespace DOCOSoft.UserAPI.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task<ResponseDto<User>?> GetUserByIdAsync(int id)
        {
            return await repository.GetUserByIdAsync(id);
        }

        public async Task<ResponseDto<User>?> GetUserByEmailAsync(LoginRequestDto loginRequestDto)
        {
            return await repository.GetUserByEmailAsync(loginRequestDto);
        }

        public async Task<ResponseDto<User>> AddUserAsync(RequestCreateDto requestCreateDto)
        {
            return await repository.AddUserAsync(requestCreateDto);
        }

        public async Task<ResponseDto<User>?> UpdateUserAsync(int id, RequestUpdateDto requestUpdateDto)
        {
            return await repository.UpdateUserAsync(id, requestUpdateDto);
        }

    }

}
