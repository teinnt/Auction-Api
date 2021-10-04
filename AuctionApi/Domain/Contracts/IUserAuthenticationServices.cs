using AuctionAPI.Domain.Models.Authentication;
using AuctionAPI.Routes.Types;
using AuctionAPI.Common.Auth;
using System.Threading.Tasks;

namespace AuctionAPI.Domain.Contracts
{
    public interface IUserAuthenticationServices
    {
        Task<Response<JsonWebToken>> RegisterUser(AddUserInput input);
        Task<Response<JsonWebToken>> LoginUser(LoginInput input);
    }
}
