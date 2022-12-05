namespace SlothOrganizer.Domain.Exceptions
{
    public class InvalidCredentialsException : BaseException
    {
        public override int StatusCode { get; protected set; } = 403;
        public InvalidCredentialsException(string message) : base(message)
        { }
    }
}
