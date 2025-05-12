using MyRecipeBook.Comunication.Request;
using MyRecipeBook.Comunication.Responses;

namespace MyRecipeBook.Application.UseCases.User.Register;
public interface IRegisterUserCase
{
    Task<ResponseRegisterUserJson> ExecuteAsync(RequestRegisterUserJson request);
}
