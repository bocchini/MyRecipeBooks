using Moq;
using MyRecipeBook.Domain.Repositories;

namespace CommonTestUtilities.Repositories;
public class UnitOfWorkBuilder
{
    public static IUnitWork Build()
    {
        var unitOfWork = new Mock<IUnitWork>();      
        return unitOfWork.Object;
    }
}
