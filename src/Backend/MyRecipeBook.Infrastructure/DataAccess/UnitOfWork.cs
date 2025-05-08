using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Infrastructure.DataAccess;
public class UnitOfWork : IUnitWork
{
    private readonly MyRecipeBookDbContext _context;

    public UnitOfWork(MyRecipeBookDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}
