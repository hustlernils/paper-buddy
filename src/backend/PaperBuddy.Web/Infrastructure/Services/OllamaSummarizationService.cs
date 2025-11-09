using PaperBuddy.Web.Common.Abstractions;
using System.Text.Json;

namespace PaperBuddy.Web.Infrastructure.Services;

public class OllamaSummarizationService : ISummarizationService
{
    private readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:11434/") };
    public async Task<string> SummarizeAsync(string text)
    {
        var request = new
        {
            model = "llama3.1",
            prompt = $"Summarize the following research paper:\n\n{text}",
            stream = false
        };

        var response = await _http.PostAsJsonAsync("api/generate", request);
        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        return json.GetProperty("response").GetString() ?? string.Empty;
    }
}