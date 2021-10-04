using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AuctionAPI.Common.Models
{
    public class Company : IMongoCommon
    {
        public Company ()
        {
            Id = ObjectId.GenerateNewId().ToString();
            UId = Guid.NewGuid();
            IsDeleted = false;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid UId { get; set; }

        public string CompanyName { get; set; }
        public string? ISIN { get; set; }
        public Address? Address { get; set; }
        public DateTime? LaunchOn { get; set; }
        public string? WalletAddress { get; set; }

        public string? RepresentativeName { get; set; }
        public string? RepresentativeEmail { get; set; }
        public string? RepresentativePhoneNumber { get; set; }
        public string? RepresentativeIdUrl { get; set; }

        public string Email { get; set; }
        public string? ContactNumber { get; set; }
        public string PasswordHash { get; set; }

        public bool? IsMobilePhoneVerified { get; set; }
        public bool? IsEmailVerified { get; set; }
        public bool? IsBankAccountVerified { get; set; }
        
        public string? NumOfAuctions { get; set; }
        public string? Auctions { get; set; }
        public float? Reputation { get; set; }

        public bool IsDeleted { get; set; }
    }
}
