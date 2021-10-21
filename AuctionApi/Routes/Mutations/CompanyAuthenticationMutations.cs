using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;
using AuctionApi.Domain.Contracts;
using AuctionApi.Common.Contracts;
using AuctionApi.Common.Models;
using AuctionApi.Domain.Models.Authentication;
using AuctionApi.Common.Auth;
using AuctionApi.Routes.Types;

namespace AuctionApi.Domain.Mutations
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
