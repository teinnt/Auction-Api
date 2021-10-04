using System.Linq;
using AuctionAPI.Common.Models;
using AuctionAPI.Domain.Contracts;
using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using AuctionAPI.Common.Security;
using System;
using System.Collections.Generic;

namespace AuctionAPI.Routes.Mutations
{
    public class BidAuctionHub : Hub
    {
        private IUserAppContext _userAppContext;
        private readonly IBidAuctionServices _bidAuctionService;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Auction> _auctionRepository;

        private static Dictionary<string, string> _connectionIds = new Dictionary<string, string>();

        public BidAuctionHub(IUserAppContext userAppContext,
                        IBidAuctionServices bidAuctionService,
                        IRepository<User> userRepository,
                        IRepository<Auction> auctionRepository)
        {
            _userAppContext = userAppContext;
            _bidAuctionService = bidAuctionService;
            _userRepository = userRepository;
            _auctionRepository = auctionRepository;
        }

        public override async Task OnConnectedAsync()
        {
            string auctionId = Context.GetHttpContext().Request.Query["auctionId"].SingleOrDefault();
            
            string token = Context.GetHttpContext().Request.Query["token"].SingleOrDefault();

            var currentUserId = Context.User.Identity.Name;

            await Groups.AddToGroupAsync(Context.ConnectionId, auctionId);

            //await Clients.Group(auctionId).SendAsync("connected", auctionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                string conversationId = Context.GetHttpContext().Request.Query["auctionId"].SingleOrDefault();

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);

                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception e)
            {
                await base.OnDisconnectedAsync(exception);
            }
        }

        public async Task AddNewBid(string auctionId, int amount)
        {
            var currentUserId = Context.User.Identity.Name;
            
            Bid newBid = await _bidAuctionService.AddNewBid(auctionId, currentUserId, amount);

            Clients.Client(Context.ConnectionId).SendAsync("addNewBid", newBid);
        }

        //----------------------------------ConnectionTest-----------------------------------

        public Task BroadcastMessage(string name, string message) =>
            Clients.All.SendAsync("broadcastMessage", name, message);
            
        public Task Echo(string name, string message) =>
            Clients.Client(Context.ConnectionId)
                   .SendAsync("echo", name, $"{message} (echo from server)");
    }
}
