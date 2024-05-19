using Auto.Data.Entities.Tests;
using Auto.Enums;

namespace Auto.Helpers;

public static class BackgroundHelper
{
    public static string GetColor(Status status)
    {
        return status switch
        {
            Status.Unknown => "bg-light",
            Status.Success => "bg-success",
            Status.Fail => "bg-danger",
            Status.InProcess => "bg-secondary",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
    
    public static string GetColor(TestQuestion question)
    {
        var status =  question.Answer?.IsCorrect switch
        {
            true => Status.Success,
            false => Status.Fail,
            _ => Status.Unknown
        };

        return GetColor(status);
    }
        
    public static string GetColor(TestQuestion question, AnswerOption option)
    {
        return question.AnswerId != option.AnswerId ? GetColor(Status.Unknown) : GetColor(question);
    }
}