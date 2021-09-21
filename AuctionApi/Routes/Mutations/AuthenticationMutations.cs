using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;
using AuctionApi.Domain.Contracts;
using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using AuctionApi.Domain.Models.Authentication;
using AuctionAPI.Common.Auth;

namespace AuctionApi.Domain.Mutations
{
    [ExtendObjectType(name: "Mutation")]
    public class AuthenticationMutations
    {
        IAuthenticationServices _authenticationServices;

        public AuthenticationMutations(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        public async Task<JsonWebToken> RegisterUser(AddUserInput input)
        {
            return await _authenticationServices.RegisterUser(input);
        }

        public async Task<JsonWebToken> RegisterCompany(AddCompanyInput input)
        {
            return await _authenticationServices.RegisterCompany(input);
        }

        public async Task<JsonWebToken> LoginUser(LoginInput input)
        {
            return await _authenticationServices.LoginUser(input.Email, input.Password);
        }

        public async Task<JsonWebToken> LoginCompany(LoginInput input)
        {
            return await _authenticationServices.LoginCompany(input.Email, input.Password);
        }
    }
}
