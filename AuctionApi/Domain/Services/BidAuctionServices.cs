using AuctionAPI.Common.Models;
using AuctionAPI.Domain.Contracts;
using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionAPI.Domain.Services
{
    public class BidAuctionServices : IBidAuctionServices
    {
        private IRepository<User> _userRepository;
        private IRepository<Auction> _auctionRepository;

        public BidAuctionServices(IRepository<User> userRepository, IRepository<Auction> auctionRepository)
        {
            _userRepository = userRepository;
            _auctionRepository = auctionRepository;
        }

        public async Task<Bid> AddNewBid(string auctionId, string senderId, int amount)
        {
            Bid newBid = new Bid
            {
                BidderId = senderId,
                Amount = amount
            };

            Auction auction = (await _auctionRepository.Get(x => x.Id == auctionId)).FirstOrDefault();
            
            auction.Rounds[auction.CurrentRound].Bids.Add(new Bid()
            {
                Amount = amount,
                BidderId = senderId
            });

            auction.Rounds[auction.CurrentRound].CurrentBid = newBid;

            await _auctionRepository.Update(auction);

            return newBid;
        }

        public async Task<AuctionRound> StartNewRound(string auctionId)
        {
            Auction auction = (await _auctionRepository.Get(x => x.Id == auctionId)).FirstOrDefault();

            if (auction == null)
            {
                return null;
            }

            auction.CurrentRound++;

            await _auctionRepository.Update(auction);

            return auction.Rounds[auction.CurrentRound];
        }

        public async Task<AuctionRound> GetCurrentRound(string auctionId)
        {
            Auction auction = (await _auctionRepository.Get(x => x.Id == auctionId)).FirstOrDefault();

            if (auction == null)
            {
                return null;
            }

            return auction.Rounds[auction.CurrentRound];
        }

        public async Task<bool> EndAuction(string auctionId)
        {
            Auction auction = (await _auctionRepository.Get(x => x.Id == auctionId)).FirstOrDefault();

            if (auction == null)
            {
                return false;
            }

            auction.IsFinished = true;

            return true;
        }
    }
}
