using AuctionApi.Common.Models;
using AuctionApi.Common.Auth;
using AuctionApi.Common.Models;
using HotChocolate.Types;

namespace AuctionApi.Routes.Types
{
    public class ResponseAuthType : ObjectType<Response<JsonWebToken>>
    {
        protected override void Configure(IObjectTypeDescriptor<Response<JsonWebToken>> descriptor)
        {

        }
    }

    public class ResponseUserType : ObjectType<Response<User>>
    {
        protected override void Configure(IObjectTypeDescriptor<Response<User>> descriptor)
        {

        }
    }

    public class ResponseCompanyType : ObjectType<Response<Company>>
    {
        protected override void Configure(IObjectTypeDescriptor<Response<Company>> descriptor)
        {

        }
    }
}
