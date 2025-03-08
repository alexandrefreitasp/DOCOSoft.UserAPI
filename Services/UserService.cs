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
            var user = await repository.GetByIdAsync(id);
            return user == null
                ? new ResponseDto<User>(null, "[MSG001] User not found.")
                : new ResponseDto<User>(user);
        }

        public async Task<ResponseDto<User>?> GetUserByEmailAsync(LoginRequestDto loginRequestDto)
        {
            var user = await repository.GetByEmailAsync(loginRequestDto.Email);
            return user == null
                ? new ResponseDto<User>(null, "[MSG003] User not found.")
                : new ResponseDto<User>(user);
        }

        public async Task<ResponseDto<User>> AddUserAsync(RequestCreateDto dto)
        {
            var existingUser = await repository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                return new ResponseDto<User>(null, "[MSG016] User with this email already exists.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            var createdUser = await repository.AddAsync(user);
            return new ResponseDto<User>(createdUser, "User account has been successfully created.");
        }

        public async Task<ResponseDto<User>?> UpdateUserAsync(int id, RequestUpdateDto dto)
        {
            var existingUser = await repository.GetByIdAsync(id);
            if (existingUser == null)
                return new ResponseDto<User>(null, "[MSG006] User not found.");

            existingUser.Name = dto.Name;
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var updatedUser = await repository.UpdateAsync(existingUser);
            return new ResponseDto<User>(updatedUser, "User information updated successfully.");
        }
    }
}
