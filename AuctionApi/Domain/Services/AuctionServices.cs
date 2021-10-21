using AuctionApi.Common.Models;
using AuctionApi.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionApi.Domain.Contracts;
using AuctionApi.Common.Utils;
using AuctionApi.Domain.Models.Auction;
using System.Security.Claims;
using AuctionApi.Domain.Models.Authentication;

namespace AuctionApi.Domain.Services
{
    public class AuctionServices : IAuctionServices
    {
        private IRepository<Auction> _auctionRepository;
        private IUserAuthenticationServices _authenticationServices;

        public AuctionServices(
            IRepository<Auction> auctionRepository,
            IUserAuthenticationServices authenticationServices) 
        {
            _auctionRepository = auctionRepository;
            _authenticationServices = authenticationServices;
        }

        public async Task<Auction> GetAuction(string auctionId)
        {
            return (await _auctionRepository.Get(x => x.Id == auctionId)).FirstOrDefault();
        }

        public async Task<List<List<Auction>>> GetAwaitingOrRunningAuctions()
        {
            var auctions = (await _auctionRepository.FindAsync(x => x.EndTime > DateTime.Now)).ToList();
            
            return CreateListByTimeShift(auctions);
        }

        public async Task<AuctionRound> UpdateWinner(ClaimsPrincipal claimsPrincipal, UpdateWinnerInput input)
        {
            var winner = await _authenticationServices.UpdateBuyerDetails(claimsPrincipal, input);

            var auction = (await _auctionRepository.FindAsync(x => x.Id == input.auctionId)).FirstOrDefault();
            int position = input.roundNumber - 1;

            var winnerId = auction?.Rounds[position].Bids.Last().BidderId;

            if (winnerId != winner.Id)
            {
                return null;
            }

            auction.Rounds[position].Winner = winner;
            auction.Rounds[position].Item.HighestPrice = auction.Rounds[position].Bids.Last().Amount;
            await _auctionRepository.Update(auction);

            return auction.Rounds[position];
        }

        public async Task<Auction> AddItemToAuction(ClaimsPrincipal claimsPrincipal, AddItemInput input)
        {
            User seller = await _authenticationServices.GetSelf(claimsPrincipal);
            
            Auction auction = (await _auctionRepository.FindAsync(x => x.Id == input.AuctionID)).FirstOrDefault();

            auction?.Rounds.Add(new AuctionRound()
            {
                Seller = seller,
                RoundNumber = auction.Rounds.Count + 1,
                Item = new Item()
                {
                    Name = input.Name,
                    Description = input.Description,
                    HighestPrice = input.Price,
                    Status = Status.READY_TO_BID,
                    TrackId = input.TrackID,
                }
            });

            // One item will take 20 minutes
            auction.EndTime = auction.EndTime.AddMinutes(20);

            await _auctionRepository.Update(auction);

            return auction;
        }

        public async Task<Auction> AddAuction(string auctionName, TimeShift timeShift)
        {
            Auction auction = new Auction() 
            { 
                Name = auctionName
            };

            AssignTime(auction, timeShift);
            //CreateRounds(auction, 2);

            auction.TimeShift = timeShift;
            auction.EndTime = auction.StartTime.AddMinutes(20);

            await _auctionRepository.Update(auction);

            return auction;
        }

        #region Helpers

        private List<List<Auction>> CreateListByTimeShift(List<Auction> auctions)
        {
            var auctionList = new List<List<Auction>>();

            var morning = new List<Auction>();
            var afternoon = new List<Auction>();
            var evening = new List<Auction>();
            var night = new List<Auction>();

            foreach (var auction in auctions)
            {
                if (auction.TimeShift == TimeShift.MORNING)
                {
                    morning.Add(auction);
                } 
                else if (auction.TimeShift == TimeShift.AFTERNOON)
                {
                    afternoon.Add(auction);
                }
                else if (auction.TimeShift == TimeShift.EVENING)
                {
                    evening.Add(auction);
                }
                else
                {
                    night.Add(auction);
                }
            }

            auctionList.Add(morning);
            auctionList.Add(afternoon);
            auctionList.Add(evening);
            auctionList.Add(night);

            return auctionList;
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
                case TimeShift.NIGHT:
                    auction.StartTime = DateTime.Today.AddHours(23);
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

        #endregion
    }
}
