using Auto.Data.Entities.Tests;

namespace Auto.Importer;

public class QuestionImportModel : Question
{
    public string? ImagePath { get; set; }
    
    public string TicketName { get; set; }
    
    public string Topic { get; set; }

    public string TitleName { get; set; }
}