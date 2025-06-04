namespace ClinicAzure.Shared.Abstractions.Errors
{
    public abstract class Error
    {
        public string Message { get; set; }
        public ErrorType Type { get; set; }
        public Error(string message, ErrorType type)
        {
            Message = message;
            Type = type;
        }
    }
}
