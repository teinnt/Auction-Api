using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionAPI.Common.Contracts
{
    public interface IMongoEntity<TId>
    {
        TId Id { get; set; }
    }
}
