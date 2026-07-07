using ShopifyApi.Models;

namespace ShopifyApi.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
