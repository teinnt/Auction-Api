using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Models.Authentication
{
    public record EditUserInput(string Email, string Name);
}
