namespace SlothOrganizer.Domain.Exceptions
{
    public class InvalidPeriodException : BaseException
    {
        public InvalidPeriodException(string message) : base(message)
        {
        }

        public InvalidPeriodException() : base("Invalid repeating period")
        {

        }

        public override int StatusCode { get; protected set; } = 400;

    }
}
