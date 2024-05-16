namespace Auto.Data.Entities.Tests;

public class AnswerOption
{
    public int AnswerId { get; set; }

    public bool IsTrue { get; set; }

    public string Text { get; set; }
    
    public int QuestionId { get; set; }
    
    public Question? Question { get; set; }
}