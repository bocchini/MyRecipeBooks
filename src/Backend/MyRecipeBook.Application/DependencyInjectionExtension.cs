using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Criptography;
using MyRecipeBook.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddAutoMapper(services);
        AddPasswordEncripter(services, configuration);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserCase, RegisterUserCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {      
        services.AddScoped(options => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper());
    }

    private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
    {
        var addtionalKey = configuration.GetValue<string>("Settings:Password:AddtionalKey");
        services.AddScoped(options => new PasswordEncripter(addtionalKey!));
    }
}
