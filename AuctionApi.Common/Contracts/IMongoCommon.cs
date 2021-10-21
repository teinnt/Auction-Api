using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApi.Common.Contracts
{
    public interface IMongoCommon : IMongoEntity<string>
    {
        public bool IsDeleted { get; set; }
    }
}
