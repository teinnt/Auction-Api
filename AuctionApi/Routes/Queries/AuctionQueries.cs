using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using AuctionAPI.Domain.Contracts;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionAPI.Domain.Queries
{
    [ExtendObjectType(name: "Query")]
    public class AuctionQueries
    {
        IRepository<Auction> _auctionRepository;
        private readonly IAuctionServices _auctionServices;

        public AuctionQueries(IRepository<Auction> auctionRepository,
            IAuctionServices auctionServices)
        {
            _auctionRepository = auctionRepository;
            _auctionServices = auctionServices;
        }

        public async Task<Auction> GetAuction(string auctionId)
        {
            return await _auctionServices.GetAuction(auctionId);
        }

        public IQueryable<Auction> GetAuctions()
        {
            return _auctionRepository.GetQueryable();
        }
    }
}
