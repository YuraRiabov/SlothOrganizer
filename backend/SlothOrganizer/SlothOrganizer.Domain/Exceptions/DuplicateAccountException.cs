namespace SlothOrganizer.Domain.Exceptions
{
    public class DuplicateAccountException : BaseException
    {
        public override int StatusCode { get; protected set; } = 400;

        public DuplicateAccountException(string message) : base(message)
        {
            
        }

        public DuplicateAccountException() : this("Account with this email already exists")
        {

        }
    }
}
