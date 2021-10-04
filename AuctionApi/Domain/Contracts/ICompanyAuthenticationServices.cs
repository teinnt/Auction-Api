using AuctionAPI.Domain.Models.Authentication;
using AuctionAPI.Routes.Types;
using AuctionAPI.Common.Auth;
using System.Threading.Tasks;

namespace AuctionAPI.Domain.Contracts
{
    public interface ICompanyAuthenticationServices
    {
        Task<Response<JsonWebToken>> RegisterCompany(AddCompanyInput input);
        Task<Response<JsonWebToken>> LoginCompany(LoginInput input);
    }
}
