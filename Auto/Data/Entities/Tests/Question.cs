namespace Auto.Data.Entities.Tests;

public class Question
{
    public int QuestionId { get; set; }

    public int? ImageId { get; set; }

    public int CategoryId { get; set; }
    
    public string Title { get; set; }

    public string Text { get; set; }
    
    public ImageData? Image { get; set; }
    
    public Category? Category { get; set; }

    public List<AnswerOption>? AnswerOptions { get; set; }
}