using System;
using HotChocolate.Types;
using System.Threading.Tasks;
using AuctionAPI.Domain.Contracts;
using AuctionAPI.Common.Models;
using AuctionAPI.Common.Utils;

namespace AuctionAPI.Routes.Mutations
{
    [ExtendObjectType(name: "Mutation")]
    public class AuctionMutations
    {
        IAuctionServices _auctionServices;

        public AuctionMutations(IAuctionServices auctionServices)
        {
            _auctionServices = auctionServices; 
        }

        public async Task<Auction> AddAuction(string name, TimeShift timeShift)
        {
            try
            {
                return await _auctionServices.AddAuction(name, timeShift);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
