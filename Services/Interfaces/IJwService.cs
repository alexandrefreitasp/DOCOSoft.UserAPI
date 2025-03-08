namespace DOCOSoft.UserAPI.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email);
        bool ValidateToken(string token);
    }
}
