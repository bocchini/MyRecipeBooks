namespace MyRecipeBook.Domain.Entities.Base;
public abstract class EntityBase
{
    public long ID { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public bool Active { get; set; } = true;
}
