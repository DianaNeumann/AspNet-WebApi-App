namespace MPS.Domain.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message) { }
}