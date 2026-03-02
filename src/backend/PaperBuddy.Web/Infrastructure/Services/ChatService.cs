using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace PaperBuddy.Web.Infrastructure.Services;

public record ChatHistoryItem(string Role, string Content);

public class ChatService
{
    private readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:11434/") };
    private readonly ILogger<ChatService>? _logger;
    
    public ChatService(ILogger<ChatService>? logger = null)
    {
        _logger = logger;
    }
    
    public async Task<string> GetAnswerAsync(IEnumerable<ChatHistoryItem> conversationHistory)
    {
        try
        {
            var messages = new List<object>
            {
                new { role = "system", content = "You are a helpful research assistant helping the user." }
            };

            messages.AddRange(conversationHistory.Select(m => new
            {
                role = m.Role.Equals("User", StringComparison.OrdinalIgnoreCase) ? "user" : "assistant",
                content = m.Content
            }));

            var request = new
            {
                model = "llama3.1",
                messages = messages,
                stream = false
            };

            var response = await _http.PostAsJsonAsync("api/chat", request);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            return json.GetProperty("message")
                      .GetProperty("content")
                      .GetString() ?? string.Empty;
        }
        catch (HttpRequestException ex)
        {
            _logger?.LogError(ex, "Failed to get response from Ollama chat API");
            return "I apologize, but I'm having trouble connecting to the language model. Please try again later.";
        }
        catch (JsonException ex)
        {
            _logger?.LogError(ex, "Failed to parse response from Ollama chat API");
            return "I apologize, but I encountered an error processing the response. Please try again.";
        }
    }
}