namespace MyRecipeBook.Exceptions.ExceptionsBase;
public class ErrorOnValidationException : MyRecipeBookException
{
    public IList<string> ErrorsMessage { get; set; }

    public ErrorOnValidationException(IList<string> errors)
    {
        ErrorsMessage = errors;
    }
}
