using FluentValidation;
using MyRecipeBook.Comunication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register;
public class RegisterUserValidator: AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResorceMessageExceptions.Name_Empty);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResorceMessageExceptions.Email_Empty);
        RuleFor(user => user.Email).EmailAddress().WithMessage(ResorceMessageExceptions.Email_Invalid);
        RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResorceMessageExceptions.Password_Greather6);
        RuleFor(user => user.Password).NotEmpty().WithMessage(ResorceMessageExceptions.Password_Empty);
    }
}
