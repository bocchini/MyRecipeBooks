using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    { 
        AddRepositories(services);
        AddDbContext(services);
    }

    private static void AddDbContext(IServiceCollection services)
    {
        var connectionString = "Server=localhost;Database=livro_receitas;Uid=root;Pwd=rootpassword";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
        services.AddDbContext<MyRecipeBookDbContext>(options =>
            options.UseMySql(connectionString, serverVersion));
    }
 
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    }
}
