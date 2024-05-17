namespace Auto.Data.Entities.Tests;

public class Category
{
    public int CategoryId { get; set; }
    
    public string Name { get; set; }
    
    public List<Question>? Questions { get; set; }
}