

namespace AuctionApi.Routes.Types
{
    public class Response<T>
    {
        public T? Data { get; set; }

        public ErrorModel? Error { get; set; }
    }

    public class ErrorModel
    {
        public string Message { get; set; }

        public string Code { get; set; }
    }
}
