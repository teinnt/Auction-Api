using AuctionApi.Common.Contracts;
using AuctionApi.Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace AuctionApi.Common.Models
{
    public enum Status
    {
        IN_AUCTION,
        SHIPPING,
        RECEIVED,
        IDLE,
        READY_TO_BID
    }

    public class Item : IMongoCommon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid UId { get; set; }
        public string TrackId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderInAuction { get; set; }

        public float HighestPrice { get; set; }
        public string Winner { get; set; }

        public List<Dictionary<User, float>> Bids { get; set; }

        public string CommissionPercent { get; set; }
        public string CommissionAmount { get; set; }

        public DateTime CreatedOn { get; set; }
        public Status Status { get; set; }
        public bool IsDeleted { get; set; }

        public Item()
        {
            Id = ObjectId.GenerateNewId().ToString();
            UId = Guid.NewGuid();
            Bids = new List<Dictionary<User, float>>();
            CreatedOn = DateTime.UtcNow;
        }
    }
}


