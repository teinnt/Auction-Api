using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionAPI.Domain.Models.Authentication
{
    public record EditUserInput(string Email, string Name);
}
