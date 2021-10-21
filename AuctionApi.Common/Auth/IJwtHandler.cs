using AuctionApi.Common.Models;
using System.Security.Claims;

namespace AuctionApi.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string userId, UserRole userRole);
        ClaimsPrincipal ValidateToken(string token);
    }
}
