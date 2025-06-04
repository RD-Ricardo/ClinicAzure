namespace ClinicAzure.Shared.Abstractions.Errors
{
    public class ForbiddenError : Error
    {
        public ForbiddenError(string message) : base(message, ErrorType.Forbidden) { }
    }
}
