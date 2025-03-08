using DOCOSoft.UserAPI.Data.Interfaces;
using DOCOSoft.UserAPI.DTOs;
using DOCOSoft.UserAPI.Models;
using DOCOSoft.UserAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace DOCOSoft.UserAPI.Data
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(int id) =>
                  await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetByEmailAsync(string email) =>
            await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User> AddAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existingUser = await context.Users.FindAsync(user.Id);
            if (existingUser == null) return null;

            context.Entry(existingUser).CurrentValues.SetValues(user);
            await context.SaveChangesAsync();

            return existingUser;
        }
    }
}
