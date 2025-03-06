namespace MyRecipeBook.Exceptions.ExceptionsBase;
public class ErrorOnValidationExcetpion : MyRecipeBookException
{
    public IList<string> ErrorMessage { get; set; }

    public ErrorOnValidationExcetpion(IList<string> errorsMessages)
    {
        ErrorMessage = errorsMessages;
    }
}
