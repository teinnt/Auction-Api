using AuctionApi.Domain.Models.Authentication;
using AuctionAPI.Common.Auth;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Contracts
{
    public interface IAuthenticationServices
    {
        Task<JsonWebToken> RegisterCompany(AddCompanyInput input);
        Task<JsonWebToken> RegisterUser(AddUserInput input);
        Task<JsonWebToken> LoginCompany(string email, string password);
        Task<JsonWebToken> LoginUser(string email, string password);
    }
}
