using System.Threading.Tasks;
using HotChocolate.Types;
using AuctionApi.Domain.Contracts;
using AuctionApi.Domain.Models.Authentication;
using AuctionApi.Common.Auth;
using AuctionApi.Routes.Types;
using System.Security.Claims;
using HotChocolate.AspNetCore.Authorization;
using AuctionApi.Common.Models;

namespace AuctionApi.Domain.Mutations
{
    [ExtendObjectType(name: "Mutation")]
    public class UserAuthenticationMutations
    {
        IUserAuthenticationServices _authenticationServices;

        public UserAuthenticationMutations(IUserAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        public async Task<Response<JsonWebToken>> RegisterUser(AddUserInput input)
        {
            return await _authenticationServices.RegisterUser(input);
        }

        public async Task<Response<JsonWebToken>> LoginUser(LoginInput input)
        {
            return await _authenticationServices.LoginUser(input);
        }

        [Authorize]
        public async Task<User> BecomeSeller(ClaimsPrincipal claimsPrincipal, UpdateUserDetailsInput input)
        {
            return await _authenticationServices.BecomeSeller(claimsPrincipal, input);
        }
    }
}
