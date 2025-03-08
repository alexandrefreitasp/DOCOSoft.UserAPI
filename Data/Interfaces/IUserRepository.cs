﻿using DOCOSoft.UserAPI.DTOs;
using DOCOSoft.UserAPI.Models;

namespace DOCOSoft.UserAPI.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<User?> UpdateAsync(User user);

    }
}
