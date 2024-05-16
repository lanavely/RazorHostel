namespace Auto.Data.Entities.Tests;

public class Question
{
    public int QuestionId { get; set; }

    public string CategoryId { get; set; }
    
    public string Name { get; set; }
    
    public string Text { get; set; }

    public int? IdImage { get; set; }
    
    public Image? Image { get; set; }
    
    public List<AnswerOption>? AnswerOptions { get; set; }
}