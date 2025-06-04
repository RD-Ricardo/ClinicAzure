namespace ClinicAzure.Shared.Abstractions.Errors
{
    public class NotFoundError : Error
    {
        public NotFoundError(string message) : base(message, ErrorType.NotFound) { }
    }
}
