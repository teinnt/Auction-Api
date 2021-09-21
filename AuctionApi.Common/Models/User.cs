using AuctionApi.Common.Models;
using AuctionAPI.Common.Contracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace AuctionAPI.Common.Models
{
    public enum UserRole
    {
        AuctionHost,
        User,
    }

    public class User : IMongoCommon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid UId { get; set; }

        public string UserName { get; set; }
        public UserRole UserRole { get; set; }

        public string LegalIdUrl { get; set; }
        public Address Address { get; set; }

        public List<string> SoldItemIds { get; set; }
        public List<string> BoughtItemIds { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }

        public bool IsMobilePhoneVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsBankAccountVerified{ get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
