namespace PaperBuddy.Web.Common.Abstractions;

public interface ISummarizationService
{
    public Task<string> SummarizeAsync(string text);

}