using System.Drawing;
using System.Text.RegularExpressions;
using Auto.Data;
using Auto.Data.Entities.Tests;
using Newtonsoft.Json;

namespace Auto.Importer;

public class Importer
{
    private readonly ApplicationDbContext _context;

    public Importer(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task ImportAsync()
    {
        var rootPath = @"C:\Users\legoj\ln\ani\pdd-russia-master\pdd-russia-master";
        var filePath = rootPath + @"\questions.json";
        
        var contents = await File.ReadAllTextAsync(filePath);
        var questions = JsonConvert.DeserializeObject<List<QuestionImportModel>>(contents);
        
        var regex = new Regex(@"[\d-]+$");
        foreach (var question in questions)
        {
            question.TicketNumber = int.Parse(regex.Match(question.TicketName).Groups[0].Value);
            question.QuestionNumber = int.Parse(regex.Match(question.TitleName).Groups[0].Value);

            if (question.ImagePath is null)
            {
                continue;
            }

            var path = Path.GetFullPath(rootPath + question.ImagePath);

            await using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var image = Image.FromStream(fs);
            using var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);

            var imageEntity = new ImageData()
            {
                Data = ms.ToArray()
            };

            question.Image = imageEntity;
        }

        var categories = questions.Select(q => q.Topic).Distinct().Select(n => new Category()
        {
            Name = n,
            Questions = questions.Where(q => q.Topic == n).Select(q => q as Question).ToList()
        });

        _context.Categories.AddRange(categories);

        await _context.SaveChangesAsync();
    }
}