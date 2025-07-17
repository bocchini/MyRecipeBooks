using CommonTestUtilities.Criptografia;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace Unit.Tests.Application.UseCases.User.Register;
public class RegisterUserCaseTest
{

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
       
        var useCase = CreateUserCase();

        var result = await useCase.ExecuteAsync(request);

        result.ShouldSatisfyAllConditions(
            () => result.ShouldNotBeNull(),
            () => result.Name.ShouldBe(request.Name)
        );       
    }

    [Fact]
    public async Task Erro_Quando_Email_Registrado()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUserCase(request.Email);

        var action = await Assert.ThrowsAsync<ErrorOnValidationException>(async ()=> await useCase.ExecuteAsync(request));

        action.ShouldSatisfyAllConditions(
            () => action.ShouldNotBeNull(),
            () => action.ErrorsMessage.Count.ShouldBe(1),
            () => action.ErrorsMessage.Contains(ResourceMessagesException.EMAIL_EXIST)
        );        
    }

    [Fact]
    public async Task Erro_Quando_Nome_Vazio()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUserCase();

        var action = await Assert.ThrowsAsync<ErrorOnValidationException>(async () => 
            await useCase.ExecuteAsync(request));

        action.ShouldSatisfyAllConditions(
            () => action.ShouldNotBeNull(),
            () => action.ErrorsMessage.Count.ShouldBe(1),
            () => action.ErrorsMessage.Contains(ResourceMessagesException.NAME_EMPTY)
        );
    }

    private RegisterUserCase CreateUserCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

        if(string.IsNullOrEmpty(email) == false)
        {
            readRepositoryBuilder.ExistActiveUserWithEmail(email);
        }

        return new RegisterUserCase(
            readRepositoryBuilder.Build(),
            writeRepository,
            mapper,
            passwordEncripter,
            unitOfWork
       );
    }
}
