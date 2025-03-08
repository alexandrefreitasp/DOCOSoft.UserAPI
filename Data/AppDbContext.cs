using DOCOSoft.UserAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DOCOSoft.UserAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
