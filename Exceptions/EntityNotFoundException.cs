namespace Bike_Store_App_WebApi.Exceptions
{
    public class EntityNotFoundException:Exception
    {
        public EntityNotFoundException() : base() { }
        public EntityNotFoundException(string message) : base(message) { }
    }
}
