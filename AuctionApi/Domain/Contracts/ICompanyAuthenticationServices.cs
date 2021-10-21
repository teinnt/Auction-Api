using AuctionApi.Domain.Models.Authentication;
using AuctionApi.Routes.Types;
using AuctionApi.Common.Auth;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Contracts
{
    public interface ICompanyAuthenticationServices
    {
        Task<Response<JsonWebToken>> RegisterCompany(AddCompanyInput input);
        Task<Response<JsonWebToken>> LoginCompany(LoginInput input);
    }
}
