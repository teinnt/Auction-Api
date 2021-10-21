
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionApi.Common.Models
{
    public class Address
    {
        public string HouseNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
