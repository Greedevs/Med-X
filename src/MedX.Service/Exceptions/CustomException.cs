namespace MedX.Service.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; } = 404;
        public CustomException(int statusCode,string message) : base(message)
        { 
            this.StatusCode = statusCode;
        }
    }
}
