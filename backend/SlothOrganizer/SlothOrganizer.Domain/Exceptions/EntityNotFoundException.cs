namespace SlothOrganizer.Domain.Exceptions
{
    public class EntityNotFoundException : BaseException
    {
        public override int StatusCode { get; protected set; } = 404;
        public EntityNotFoundException(string message) : base(message)
        {

        }
    }
}
