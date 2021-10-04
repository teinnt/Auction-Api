using AuctionAPI.Common.Models;
using AuctionAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionAPI.Domain.Contracts
{
    public interface IBidAuctionServices
    {
        Task<AuctionRound> StartNewRound(string auctionId);
        Task<AuctionRound> GetCurrentRound(string auctionId);
        Task<Bid> AddNewBid(string auctionId, string senderId, int amount);
    }
}
