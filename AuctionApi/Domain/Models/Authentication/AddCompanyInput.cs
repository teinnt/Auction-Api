using AuctionApi.Common.Models;
using AuctionApi.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Models.Authentication
{
    public record AddCompanyInput(
        string CompanyName,
        string Isin,
        string HouseNumber,
        string StreetAddress,
        string WalletAddress,
        string City,
        string State,
        string Country,
        string ZipCode,
        string Email,
        string Password,
        string RepresentativeName,
        string RepresentativeEmail,
        string RepresentativePhoneNumber,
        string RepresentativeIdUrl,
        string ContactNumber,
        DateTime LaunchOn
    );
}