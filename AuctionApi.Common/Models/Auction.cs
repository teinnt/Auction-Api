using AuctionApi.Common.Contracts;
using AuctionApi.Common.Models;
using AuctionApi.Common.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace AuctionApi.Common.Models
{
    public class Bid 
    {
        public string BidderId { get; set; }
        public float Amount { get; set; }
    }

    public class AuctionRound
    {
        public string Id { get; set; }
        public int RoundNumber { get; set; }

        public Item Item { get; set; }
        public User Winner { get; set; }
        public User Seller { get; set; }
        public Bid CurrentBid { get; set; }
        public List<Bid> Bids { get; set; }

        public AuctionRound()
        {
            Id = ObjectId.GenerateNewId().ToString();
            Bids = new List<Bid>();
        }
    }

    public class Auction : IMongoCommon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid UId { get; set; }

        public string Name { get; set; }
        public TimeShift TimeShift { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartTime { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndTime { get; set; }

        public string Company { get; set; }
        public string HostPerson { get; set; }
        public string TotalCommissionAmount { get; set; }
        public string TotalSellAmount { get; set; }
        public float RestrictAmount { get; set; }
        
        public List<AuctionRound> Rounds { get; set; }
        public int CurrentRound { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsFinished { get; set; }
        public bool IsDeleted { get; set; }

        public Auction()
        {
            Id = ObjectId.GenerateNewId().ToString();
            UId = Guid.NewGuid();
            Rounds = new List<AuctionRound>();
        }
    }
}


