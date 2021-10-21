using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Models.Auction
{
    public record AddItemInput(
        string Name, 
        float Price,
        string Location,
        string TrackID,
        string ImageURL,
        string Description,
        string OwnerWalletAddress,
        string AuctionID
    );
}
    