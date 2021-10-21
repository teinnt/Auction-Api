using System;
using HotChocolate.Types;
using System.Threading.Tasks;
using AuctionApi.Domain.Contracts;
using AuctionApi.Common.Models;
using AuctionApi.Common.Utils;
using AuctionApi.Domain.Models.Auction;
using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuctionApi.Routes.Mutations
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
            return await _auctionServices.AddAuction(name, timeShift);
        }

        [Authorize]
        public async Task<Auction> AddItemToAuction(ClaimsPrincipal claimsPrincipal, AddItemInput input)
        {
            return await _auctionServices.AddItemToAuction(claimsPrincipal, input);
        }

        [Authorize]
        public async Task<AuctionRound> UpdateItemWinner(ClaimsPrincipal claimsPrincipal,
            UpdateWinnerInput input)
        {
            return await _auctionServices.UpdateWinner(claimsPrincipal, input);
        }
    }
}
