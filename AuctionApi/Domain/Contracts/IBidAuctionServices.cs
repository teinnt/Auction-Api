using AuctionApi.Common.Models;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Contracts
{
    public interface IBidAuctionServices
    {
        Task<AuctionRound> StartNewRound(string auctionId);
        Task<AuctionRound> GetCurrentRound(string auctionId);
        Task<Bid> AddNewBid(string auctionId, string senderId, int amount);
    }
}
