namespace ClinicAzure.Shared.Abstractions.Errors
{
    public class InvalidError : Error
    {
        public InvalidError(string message) : base(message, ErrorType.Invalid) { }
    }
}
