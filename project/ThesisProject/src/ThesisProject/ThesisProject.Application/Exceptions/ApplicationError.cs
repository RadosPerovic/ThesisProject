namespace ThesisProject.Application.Exceptions;
public class ApplicationError : Exception
{
    public ApplicationError(string errorMessage) : base(errorMessage)
    {

    }
}
