namespace Ecommerce.Api.Helper
{
    public class ApiException : ResponeApi
    {
        public ApiException(int statusCode, string? message = null,string details =null) : base(statusCode, message)
        {
            details = Details;
        }

        public string Details { get; set; }
    }
}
