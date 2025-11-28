using api.Models;

namespace api.Interfaces
{
    public interface ITokenService
    {
        (string accessToken, string refreshToken) GenerateToken(AppUser user);
        string GenerateRefreshToken();
    }
}