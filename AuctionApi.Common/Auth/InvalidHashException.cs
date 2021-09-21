using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionAPI.Common.Auth
{
    public class InvalidHashException : Exception
    {
        public InvalidHashException() { }
        public InvalidHashException(string message)
            : base(message) { }
        public InvalidHashException(string message, Exception inner)
            : base(message, inner) { }
    }
}
