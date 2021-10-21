using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApi.Common.Auth
{
    public class JsonWebToken
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public long Expires { get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; }
        public string WalletAddress { get; set; }
    }
}