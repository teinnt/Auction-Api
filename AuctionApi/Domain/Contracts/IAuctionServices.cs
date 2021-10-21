using AuctionApi.Common.Models;
using AuctionApi.Common.Utils;
using AuctionApi.Domain.Models.Auction;
using AuctionApi.Domain.Models.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Contracts
{
    public interface IAuctionServices
    {
        Task<Auction> GetAuction(string auctionId);
        Task<Auction> AddAuction(string name, TimeShift timeShift);
        Task<List<List<Auction>>> GetAwaitingOrRunningAuctions();
        Task<Auction> AddItemToAuction(ClaimsPrincipal claimsPrincipal, AddItemInput input);
        Task<AuctionRound> UpdateWinner(ClaimsPrincipal claimsPrincipal, UpdateWinnerInput input);
    }
}
