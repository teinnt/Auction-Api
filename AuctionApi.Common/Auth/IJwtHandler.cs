using AuctionAPI.Common.Models;
using System.Security.Claims;

namespace AuctionAPI.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string userId, UserRole userRole);
        ClaimsPrincipal ValidateToken(string token);
    }
}
