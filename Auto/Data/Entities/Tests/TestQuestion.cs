namespace Auto.Data.Entities.Tests;

public class TestQuestion
{
    public int IdTestQuestion { get; set; }
    
    public int IdTest { get; set; }
    
    public int IdQuestion { get; set; }

    public int? IdSelectedAnswer { get; set; }

    public Test Test { get; set; }
    
    public Question Question { get; set; }
    
    public AnswerOption? Answer { get; set; }
}