namespace PaperBuddy.Web.Common.Abstractions;

public interface IEmbeddingService
{
    public Task<float[]> GetEmbeddingAsync(string text);
}