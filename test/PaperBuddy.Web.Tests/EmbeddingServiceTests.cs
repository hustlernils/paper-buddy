using PaperBuddy.Web.Infrastructure.Services;

namespace PaperBuddy.Web.Tests;

public class EmbeddingServiceTests
{
    [Fact]
    public async Task GetEmbeddingAsync_Returns_VectorAsFloatArray()
    {
        string text = "This is an example text that is passed to the embedding service as an argument and should be returned as an array of floats, lets see how it goes";
        var service = new OllamaEmbeddingService();

        var embeddings = await service.GetEmbeddingAsync(text);
        
        Assert.IsType<float[]>(embeddings);
    }
}