using FluentValidation;
using MyRecipeBook.Comunication.Request;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.NAME_EMPTY);
        When(user => !string.IsNullOrEmpty(user.Name), () =>
        {
            RuleFor(x => x.Name)
            .Length(2, 50)
            .WithMessage(ResourceMessagesException.NAME_MORE_EQUAL);

        });
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.EMAIL_EMPTY);            
        When(user => !string.IsNullOrEmpty(user.Email), () =>
        {
            RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.PASSWORD_EMPTY);
        RuleFor(x => x.Password.Length)
           .GreaterThanOrEqualTo(6)
            .WithMessage(ResourceMessagesException.PASSWORD_MORE_EQUAL);
    }
}
