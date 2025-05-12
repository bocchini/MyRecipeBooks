using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Criptography;
public class PasswordEncripter
{
    private readonly string _chaveAdicional;

    public PasswordEncripter(string chaveAdicional)
    {
        _chaveAdicional = chaveAdicional;
    }

    public string Encrypt(string password)
    {
        var newPassword = $"{password} {_chaveAdicional}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    public static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }
}
