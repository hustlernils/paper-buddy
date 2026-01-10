using System.Text.Json;

namespace PaperBuddy.Web.Infrastructure.Services;

public class ChatService
{
    private readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:11434/") };
    
    public async Task<string> GetAnswerAsync(string chatMessage)
    {
        var request = new
        {
            model = "llama3.1",
            prompt = chatMessage,
            stream = false
        };

        var response = await _http.PostAsJsonAsync("api/generate", request);
        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        return json.GetProperty("response").GetString() ?? string.Empty;
    }
}