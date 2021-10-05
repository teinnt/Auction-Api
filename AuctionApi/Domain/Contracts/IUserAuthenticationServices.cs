using AuctionAPI.Domain.Models.Authentication;
using AuctionAPI.Routes.Types;
using AuctionAPI.Common.Auth;
using System.Threading.Tasks;
using AuctionAPI.Common.Models;
using System.Security.Claims;

namespace AuctionAPI.Domain.Contracts
{
    public interface IUserAuthenticationServices
    {
        Task<User> GetSelf(ClaimsPrincipal claimsPrincipal);
        Task<Response<JsonWebToken>> LoginUser(LoginInput input);
        Task<Response<JsonWebToken>> RegisterUser(AddUserInput input);
    }
}
