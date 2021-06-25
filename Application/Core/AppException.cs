namespace Application.Core
{
    public class AppException
    {
        public AppException(string message, int statusCode, string details = null)
        {
            Message = message;
            StatusCode = statusCode;
            Details = details;
        }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        //stact-trace (only for development)
        public string Details { get; set; }
    }
}