using System.Threading.Tasks;
using HotChocolate.Types;
using AuctionAPI.Domain.Contracts;
using AuctionAPI.Domain.Models.Authentication;
using AuctionAPI.Common.Auth;
using AuctionAPI.Routes.Types;

namespace AuctionAPI.Domain.Mutations
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
    }
}
