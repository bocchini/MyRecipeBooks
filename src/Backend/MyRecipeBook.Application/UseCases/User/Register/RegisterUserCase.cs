using AutoMapper;
using MyRecipeBook.Application.Services.Criptography;
using MyRecipeBook.Comunication.Request;
using MyRecipeBook.Comunication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;
public class RegisterUserCase: IRegisterUserCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitWork _unitWork;
    private readonly IMapper _mapper;
    private readonly PasswordEncripter _passwordEncripter;

    public RegisterUserCase(IUserReadOnlyRepository userReadOnlyRepository, IUserWriteOnlyRepository userWriteOnlyRepository, IMapper mapper, PasswordEncripter passwordEncripter, IUnitWork unitWork)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _unitWork = unitWork;
    }

    public async Task<ResponseRegisterUserJson> ExecuteAsync(RequestRegisterUserJson request)
    {
        Validate(request);
               
        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);

        await _userWriteOnlyRepository.AddAsync(user);
        await _unitWork.Commit();
        return new ResponseRegisterUserJson
        {
            Name = request.Name,
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
