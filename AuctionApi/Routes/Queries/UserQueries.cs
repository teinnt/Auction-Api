using AuctionApi.Common.Contracts;
using AuctionApi.Common.Models;
using AuctionApi.Domain.Contracts;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Queries
{
    [ExtendObjectType(name: "Query")]
    public class UserQueries
    {
        IRepository<User> _userRepository;
        IUserAuthenticationServices _authenticationServices;

        public UserQueries(IRepository<User> userRepository,
                        IUserAuthenticationServices authenticationServices)
        {
            _userRepository = userRepository;
            _authenticationServices = authenticationServices;
        }

        [Authorize]
        public async Task<User> GetSelf(ClaimsPrincipal claimsPrincipal)
        {
            return await _authenticationServices.GetSelf(claimsPrincipal);
        }

        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetQueryable();
        }
    }
}
