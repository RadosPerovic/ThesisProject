namespace ThesisProject.Domain.Exceptions;
public class DomainError : Exception
{
    public DomainError(string errorMessage)
        : base(errorMessage)
    {

    }
}
