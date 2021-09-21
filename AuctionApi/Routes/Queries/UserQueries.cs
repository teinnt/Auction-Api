using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace AuctionApi.Domain.Queries
{
    [ExtendObjectType(name: "Query")]
    public class UserQueries
    {
        IRepository<User> _userRepository;

        public UserQueries(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetQueryable();
        }
    }
}
