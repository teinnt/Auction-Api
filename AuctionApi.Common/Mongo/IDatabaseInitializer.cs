﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionAPI.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
