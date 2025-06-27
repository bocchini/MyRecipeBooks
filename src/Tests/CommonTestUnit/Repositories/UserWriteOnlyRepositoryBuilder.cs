using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;
public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        var userWriteOnlyRepository = new Mock<IUserWriteOnlyRepository>();
     
        return userWriteOnlyRepository.Object;
    }
}
