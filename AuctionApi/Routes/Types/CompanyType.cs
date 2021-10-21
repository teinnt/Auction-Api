    using AuctionApi.Common.Models;
using AuctionApi.Common.Models;
using AuctionApi.Common.Services;
using HotChocolate;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionApi.Routes.Types
{
    public class CompanyType : ObjectType<Company>
    {
        protected override void Configure(IObjectTypeDescriptor<Company> descriptor)
        {
            descriptor.Field(s => s.LaunchOn).Type<NonNullType<DateTimeType>>();
        }
    }
}
