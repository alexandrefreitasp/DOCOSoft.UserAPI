using DOCOSoft.UserAPI.Data.Interfaces;
using DOCOSoft.UserAPI.DTOs;
using DOCOSoft.UserAPI.Models;
using DOCOSoft.UserAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace DOCOSoft.UserAPI.Data
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<ResponseDto<User>?> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

                return user == null ? new ResponseDto<User>(null, "[MSG001] User not found.") : new ResponseDto<User>(user);
            }
            catch
            {
                return new ResponseDto<User>(null, "[MSG002] An error occurred while retrieving user information. Please try again later.");
            }
        }

        public async Task<ResponseDto<User>?> GetUserByEmailAsync(LoginRequestDto loginRequestDto)
        {
            try
            {
                var user =  await context.Users.AsNoTracking().FirstOrDefaultAsync(u=> u.Email == loginRequestDto.Email);

                if (user == null)
                    return new ResponseDto<User>(null, "[MSG003] User not found."); 
                        
                if (!BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.PasswordHash))
                    return new ResponseDto<User>(null, "[MSG013] Invalid email or password");

                return new ResponseDto<User>(user);
            }
            catch
            {
                return new ResponseDto<User>(null, "[MSG004] An error occurred while retrieving user information. Please try again later.");
            }
        }

        public async Task<ResponseDto<User>> AddUserAsync(RequestCreateDto requestCreateDto)
        {
            try
            {
                var checkExists =  await context.Users.AsNoTracking().FirstOrDefaultAsync(u=> u.Email == requestCreateDto.Email);
                
                if (checkExists != null) return new ResponseDto<User>(null, "[MSG016]  User with this email already exists.");

                var user = new User
                {
                    Name = requestCreateDto.Name,
                    Email = requestCreateDto.Email,
                    PasswordHash = EncryptPasswordService.HashPassword(requestCreateDto.Password)
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return new  ResponseDto<User>(user,"User account has been successfully created.");
            }
            catch 
            {
                return new ResponseDto<User>(null, "[MSG005] An error occurred while registering the user. Please try again later.");
            }
        }

        public async Task<ResponseDto<User>> UpdateUserAsync(int id, RequestUpdateDto requestUpdateDto)
        {
            try
            {
                var user =  await context.Users.FindAsync(id);
                
                if (user == null) return new ResponseDto<User>(null, "[MSG006]  User not found.");

                user.Name = requestUpdateDto.Name;
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(requestUpdateDto.Password);

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return new  ResponseDto<User>(user,"User information updated successfully.");

            }
            catch 
            {

                return new ResponseDto<User>(null, "[MSG007] An error occurred while updating the user. Please try again later.");

            }
        }
    }
}
