using AuctionAPI.Common.Models;
using AuctionAPI.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using AuctionAPI.Domain.Contracts;
using AuctionAPI.Common.Utils;

namespace AuctionAPI.Domain.Services
{
    public class AuctionServices : IAuctionServices
    {
        private IRepository<User> _userRepository;
        private IRepository<Auction> _auctionRepository;

        public AuctionServices(IRepository<User> userRepository, IRepository<Auction> auctionRepository)
        {
            _userRepository = userRepository;
            _auctionRepository = auctionRepository;
        }

        public async Task<Auction> GetAuction(string auctionId)
        {
            return (await _auctionRepository.Get(x => x.Id == auctionId)).FirstOrDefault();
        }

        public async Task<Auction> AddAuction(string auctionName, TimeShift timeShift)
        {
            Auction auction = new Auction() 
            { 
                Name = auctionName
            };

            AssignTime(auction, timeShift);
            CreateRounds(auction, 10);

            await _auctionRepository.Update(auction);

            return auction;
        }

        private void AssignTime(Auction auction, TimeShift timeShift)
        {
            switch (timeShift)
            {
                case TimeShift.MORNING:
                    auction.StartTime = DateTime.Today.AddHours(10);
                    break;
                case TimeShift.AFTERNOON:
                    auction.StartTime = DateTime.Today.AddHours(15);
                    break;
                case TimeShift.EVENING:
                    auction.StartTime = DateTime.Today.AddHours(19);
                    break;
                default:
                    break;
            }
        }

        private void CreateRounds(Auction auction, int numOfRounds)
        {
            for (int i = 1; i <= numOfRounds; i++)
            {
                auction.Rounds.Add(new AuctionRound()
                {
                    RoundNumber = i,
                    Item = new Item()
                    {
                        Name = "Name " + i.ToString(),
                        Description = "Lorem20" + i.ToString()
                    }
                });
            }
        }
    }
}
