using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Comunication.Request;
using MyRecipeBook.Comunication.Responses;

namespace MyRecipeBook.API.Controllers;
[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisterUserJson request)
    {

        var result = new RegisterUserCase().Execute(request);

        return Created(string.Empty, result);
    }
}
