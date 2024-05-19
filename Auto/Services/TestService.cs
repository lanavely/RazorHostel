using Auto.Data;
using Auto.Data.Entities;
using Auto.Data.Entities.Tests;
using Auto.Models;
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

        //_context.Tests.Add(test);
        //await _context.SaveChangesAsync();

        return test;
    }

    public async Task<Test> CreateTestForTicket(int ticketNumber, AppUser user)
    {
        var questions = await _context.Questions
            .Where(q => q.TicketNumber == ticketNumber)
            .OrderBy(q => q.QuestionNumber)
            .Include(q => q.AnswerOptions)
            .Include(q => q.Image)
            .ToListAsync();

        var test = new Test()
        {
            TicketNumber = ticketNumber,
            Date = DateTime.Now,
            User = user,
            Questions = questions.Select(q =>
                new TestQuestion()
                {
                    Question = q
                }).ToList()
        };

        _context.Tests.Add(test);
        await _context.SaveChangesAsync();

        return test;
    }
    
    public async Task<List<Test>> GetTestForTicketsAsync(AppUser user)
    {
        var tests = await _context.Tests
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answer)
            .Where(q => q.UserId == user.Id)
            .GroupBy(t => t.TicketNumber)
            .Select(g => g.OrderByDescending(g => g.Date).First())
            .ToListAsync();

        return tests;
    }

    public async Task<Test> GetTestByIdAsync(int testId)
    {
        var test = await _context.Tests
            .Where(t => t.TestId == testId)
            .Include(t => t.Questions)
            .ThenInclude(q => q.Question.AnswerOptions)
            .Include(t => t.Questions)
            .ThenInclude(q => q.Question.Image)
            .FirstAsync();

        return test;
    }
    
    public void SelectQuestion(TestQuestion question, int idAnswer)
    {
        question.Answer = question.Question.AnswerOptions.First(o => o.AnswerId == idAnswer);
    }
}