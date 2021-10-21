using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApi.Common.Contracts
{
    public interface IMongoEntity<TId>
    {
        TId Id { get; set; }
    }
}
