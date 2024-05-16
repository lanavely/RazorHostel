namespace Auto.Data.Entities.Tests;

public class QuestionCategory
{
    public int CategoryId { get; set; }
    
    public string Code { get; set; }

    public string Name { get; set; }
    
    public List<Question>? Questions { get; set; }
}