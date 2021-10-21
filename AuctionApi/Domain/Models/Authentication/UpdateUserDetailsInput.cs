using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Models.Authentication
{
    public record UpdateUserDetailsInput(
       string PhoneNumber,
       string HouseNumber,
       string Street,
       string City,
       string State,
       string Country,
       string ZipCode,
       string ImageURL,
       string WalletAddress
   );
}
