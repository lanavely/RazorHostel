using Auto.Data;
using Auto.Data.Entities.Tests;
using Microsoft.EntityFrameworkCore;

namespace Auto.Services;

public class TestService
{
    private readonly ApplicationDbContext _context;

    public TestService(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Test> GetTestAsync()
    {
        var categories = await _context.QuestionCategories.ToListAsync();
        var questions = await _context.Questions
            .Include(c => c.Category)
            .Include(c => c.AnswerOptions)
            .GroupBy(e => e.CategoryId)
            .Select(c => new
            {
                c.Key,
                Questions = c.OrderBy(r => Guid.NewGuid()).Take(5)
            })
            .SelectMany(c => c.Questions)
            .Select(q => new TestQuestion()
            {
                Question = q
            })
            .ToListAsync();

        var test = new Test()
        {
            Questions = questions
        };

        _context.Tests.Add(test);
        await _context.SaveChangesAsync();

        return test;
    }

    public void SelectQuestion(TestQuestion question, int idAnswer)
    {
        question.Answer = question.Question.AnswerOptions.First(o => o.AnswerId == idAnswer);
    }
}