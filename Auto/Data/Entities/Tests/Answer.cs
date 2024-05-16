namespace Auto.Data.Entities.Tests;

public class Answer
{
    public string IdAnswer { get; set; }

    public int IdAnswerOption { get; set; }
    
    public bool IsSelected { get; set; }
    
    public AnswerOption? AnswerOption { get; set; }
}