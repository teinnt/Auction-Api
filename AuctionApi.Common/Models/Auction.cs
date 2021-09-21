using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AuctionApi.Common.Models
{
    public class Auction : IMongoCommon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid UId { get; set; }

        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Company { get; set; }
        public string HostPerson { get; set; }
        public string TotalCommissionAmount { get; set; }
        public string TotalSellAmount { get; set; }
        public float RestrictAmount { get; set; }
        
        public List<User> Bidders { get; set; }
        public List<Dictionary<Item, User>> Items { get; set; }
        public DateTime CreatedOn { get; set; }

        public bool IsCancelled { get; set; }
        public bool IsFinished { get; set; }
        public bool IsDeleted { get; set; }

        public Auction()
        {
            Bidders = new List<User>();
            Items = new List<Dictionary<Item, User>>();
        }
    }
}


