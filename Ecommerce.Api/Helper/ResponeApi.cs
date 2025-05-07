namespace Ecommerce.Api.Helper
{
    public class ResponeApi
    {
        public ResponeApi(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageFromStatusCode(StatusCode);
        }

        public int StatusCode { get; set; }
        public string?  Message { get; set; }

        private string GetMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Un Authorized",
                500 => "Server Error",
                _=> null!
            };
        }
    }
}
