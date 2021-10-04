using AuctionAPI.Common.Models;
using AuctionAPI.Common.Utils;
using System.Threading.Tasks;

namespace AuctionAPI.Domain.Contracts
{
    public interface IAuctionServices
    {
        Task<Auction> GetAuction(string auctionId);
        Task<Auction> AddAuction(string name, TimeShift timeShift);
    }
}
