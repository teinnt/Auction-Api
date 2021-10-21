using AuctionApi.Domain.Models.Authentication;
using AuctionApi.Routes.Types;
using AuctionApi.Common.Auth;
using System.Threading.Tasks;
using AuctionApi.Common.Models;
using System.Security.Claims;
using AuctionApi.Domain.Models.Auction;

namespace AuctionApi.Domain.Contracts
{
    public interface IUserAuthenticationServices
    {
        Task<User> GetUserById(string userId);
        Task<User> GetSelf(ClaimsPrincipal claimsPrincipal);
        Task<Response<JsonWebToken>> LoginUser(LoginInput input);
        Task<Response<JsonWebToken>> RegisterUser(AddUserInput input);
        Task<User> BecomeSeller(ClaimsPrincipal claimsPrincipal, UpdateUserDetailsInput input);
        Task<User> UpdateBuyerDetails(ClaimsPrincipal claimsPrincipal, UpdateWinnerInput input);
    }
}
