using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;
using AuctionAPI.Domain.Contracts;
using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using AuctionAPI.Domain.Models.Authentication;
using AuctionAPI.Common.Auth;
using AuctionAPI.Routes.Types;

namespace AuctionAPI.Domain.Mutations
{
    [ExtendObjectType(name: "Mutation")]
    public class CompanyAuthenticationMutations
    {
        ICompanyAuthenticationServices _authenticationServices;

        public CompanyAuthenticationMutations(ICompanyAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        public async Task<Response<JsonWebToken>> RegisterCompany(AddCompanyInput input)
        {
            return await _authenticationServices.RegisterCompany(input);
        }

        public async Task<Response<JsonWebToken>> LoginCompany(LoginInput input)
        {
            return await _authenticationServices.LoginCompany(input);
        }
    }
}
