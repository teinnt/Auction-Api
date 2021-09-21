using AuctionApi.Common.Models;
using AuctionAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Models.Authentication
{
    public record AddUserInput (
        string UserName,
        string Email,
        string Password
    );
}