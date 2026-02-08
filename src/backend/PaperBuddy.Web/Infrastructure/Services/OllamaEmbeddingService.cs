using System.Text.Json;
using PaperBuddy.Web.Common.Abstractions;

namespace PaperBuddy.Web.Infrastructure.Services;

public class EmbedResponse
{
    public float[][] Embeddings { get; set; } = [];
}

public class OllamaEmbeddingService : IEmbeddingService
{
    private readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:11434/") };
    public async Task<float[]> GetEmbeddingAsync(string text)
    {
        var request = new
        {
            model = "nomic-embed-text",
            input = text
        };

        var response = await _http.PostAsJsonAsync("api/embed", request);
        var result = await response.Content.ReadFromJsonAsync<EmbedResponse>();

        return result!.Embeddings[0];
    }
}