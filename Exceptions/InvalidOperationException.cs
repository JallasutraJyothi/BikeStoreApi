namespace Bike_Store_App_WebApi.Exceptions
{
    public class InvalidOperationException : Exception
    {
        public InvalidOperationException() : base() { }

        public InvalidOperationException(string message) : base(message) { }
    }
}
