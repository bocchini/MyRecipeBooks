using MyRecipeBook.Application.Services.Criptography;

namespace CommonTestUtilities.Criptografia;
public class PasswordEncripterBuilder
{
    public static PasswordEncripter Build() => new PasswordEncripter("abc1234");
}
