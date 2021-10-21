using AuctionApi.Common.Models;
using AuctionApi.Common.Models;
using AuctionApi.Common.Services;
using HotChocolate;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionApi.Routes.Types
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(s => s.SoldItemIds).ResolveWith<Resolvers>(c => c.GetSoldItems(default!, default!));
            descriptor.Field(s => s.BoughtItemIds).ResolveWith<Resolvers>(c => c.GetBoughtItems(default!, default!));
        }

        private class Resolvers
        {
            public async Task<IEnumerable<Item>> GetSoldItems(User user, 
                [ScopedService] Repository<Item> itemRepository)
            {
                return await itemRepository.FindAsync(c => c.Id == user.SoldItemIds[0]);
            }

            public async Task<IEnumerable<Item>> GetBoughtItems(User user,
                [ScopedService] Repository<Item> itemRepository)
            {
                return await itemRepository.FindAsync(c => c.Id == user.BoughtItemIds[0]);
            }
        }
    }
}
