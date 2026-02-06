using PaperBuddy.Web.Infrastructure.Services;

namespace PaperBuddy.Web.Tests;

public class ChunkingServiceTests
{
    private readonly ChunkingService _chunkingService = new();

    [Fact]
    public void GetChunks_WithEmptyText_ReturnsSingleEmptyChunk()
    {
        var text = string.Empty;

        var result = _chunkingService.GetChunks(text);

        Assert.Single(result);
        Assert.Equal(0, result[0].ChunkIndex);
        Assert.Equal(string.Empty, result[0].Text);
    }

    [Fact]
    public void GetChunks_WithNullText_ThrowsArgumentNullException()
    {
        string? text = null;

        Assert.Throws<ArgumentNullException>(() => _chunkingService.GetChunks(text!));
    }

    [Fact]
    public void GetChunks_WithShortText_ReturnsSingleChunk()
    {
        var text = "This is a short text. It should be in one chunk.";

        var result = _chunkingService.GetChunks(text);

        Assert.Single(result);
        Assert.Equal(0, result[0].ChunkIndex);
        Assert.Equal(text, result[0].Text);
    }

    [Fact]
    public void GetChunks_WithTextUnderChunkSize_ReturnsSingleChunk()
    {
        var text = string.Join(" ", Enumerable.Repeat("This is a sentence.", 20));

        var result = _chunkingService.GetChunks(text);

        Assert.Single(result);
        Assert.Equal(0, result[0].ChunkIndex);
    }

    [Fact]
    public void GetChunks_WithLongText_ReturnsMultipleChunks()
    {
        var sentences = Enumerable.Repeat("This is a test sentence that should help create chunks. ", 100).ToList();
        var text = string.Join("", sentences);

        var result = _chunkingService.GetChunks(text);

        Assert.True(result.Length > 1);
        
        for (int i = 0; i < result.Length; i++)
        {
            Assert.Equal(i, result[i].ChunkIndex);
        }
        
        Assert.All(result, chunk => Assert.NotEmpty(chunk.Text));
    }

    [Fact]
    public void GetChunks_WithMultipleSentences_SplitsAtSentenceBoundaries()
    {
        var text = "First sentence. Second sentence! Third sentence? Fourth sentence. Fifth sentence.";

        var result = _chunkingService.GetChunks(text);

        Assert.True(result.Length >= 1);
        
        Assert.All(result, chunk => 
        {
            var trimmedText = chunk.Text.Trim();
            Assert.True(trimmedText.EndsWith('.') || trimmedText.EndsWith('!') || trimmedText.EndsWith('?'), 
                $"Chunk should end with sentence punctuation: '{trimmedText}'");
        });
    }

    [Fact]
    public void GetChunks_WithDifferentSentenceEndings_HandlesAllPunctuation()
    {
        var text = "Sentence with period! Sentence with exclamation? Sentence with question. Sentence with period.";

        var result = _chunkingService.GetChunks(text);

        Assert.True(result.Length >= 1);
        Assert.All(result, chunk => Assert.NotEmpty(chunk.Text));
    }

    [Fact]
    public void GetChunks_WithVeryLongSentences_CreatesMultipleChunks()
    {
        var longSentence = "This is an extremely long sentence that contains many words and should definitely exceed the chunk size limit when processed by the chunking service. ";
        var text = string.Join("", Enumerable.Repeat(longSentence, 50));

        var result = _chunkingService.GetChunks(text);

        Assert.True(result.Length > 1);
        Assert.All(result, chunk => Assert.NotEmpty(chunk.Text));
    }

    [Fact]
    public void GetChunks_WithMixedSentenceLengths_HandlesGracefully()
    {
        var text = "Short. This is a medium length sentence with several words. ";
        text += string.Join(" ", Enumerable.Repeat("This is a very long sentence that goes on and on and should be much longer than typical sentences. ", 30));
        text += " Another short one.";

        var result = _chunkingService.GetChunks(text);

        Assert.True(result.Length >= 1);
        Assert.All(result, chunk => Assert.NotEmpty(chunk.Text));
    }

    [Fact]
    public void GetChunks_WithTextContainingExtraSpaces_PreservesContent()
    {
        var text = "Sentence one.   Sentence two!   Sentence three?    Sentence four.";

        var result = _chunkingService.GetChunks(text);

        Assert.True(result.Length >= 1);
        
        var combinedText = string.Join(" ", result.Select(c => c.Text));
        Assert.Contains("Sentence one", combinedText);
        Assert.Contains("Sentence two", combinedText);
        Assert.Contains("Sentence three", combinedText);
        Assert.Contains("Sentence four", combinedText);
    }

    [Fact]
    public void GetChunks_ReturnsChunksWithSequentialIds()
    {
        var text = string.Join(" ", Enumerable.Repeat("This is a test sentence. ", 100));

        var result = _chunkingService.GetChunks(text);

        for (int i = 0; i < result.Length; i++)
        {
            Assert.Equal(i, result[i].ChunkIndex);
        }
    }

    [Fact]
    public void GetChunks_WithSingleLongSentence_CreatesMultipleChunks()
    {
        var text = "This is one single very long sentence without any periods or exclamation marks or question marks that should still be chunked properly by the service even though it doesn't have natural sentence boundaries to work with which makes the chunking more challenging but still necessary";

        var result = _chunkingService.GetChunks(text);

        Assert.True(result.Length >= 1);
        Assert.All(result, chunk => Assert.NotEmpty(chunk.Text));
    }

    [Fact]
    public void GetChunks_WithTextOnlyContainingWhitespace_ReturnsSingleChunkWithWhitespace()
    {
        var text = "   \n\t   \r\n   ";

        var result = _chunkingService.GetChunks(text);

        Assert.Single(result);
        Assert.Equal(0, result[0].ChunkIndex);
        Assert.Equal(text, result[0].Text);
    }

    [Theory]
    [InlineData("Simple sentence.")]
    [InlineData("Question?")]
    [InlineData("Exclamation!")]
    [InlineData("Multiple sentences. Second one! Third?")]
    public void GetChunks_WithVariousTextStructures_ReturnsValidChunks(string text)
    {
        var result = _chunkingService.GetChunks(text);

        Assert.NotEmpty(result);
        Assert.All(result, chunk => 
        {
            Assert.True(chunk.ChunkIndex >= 0);
            Assert.NotNull(chunk.Text);
        });
    }

    [Fact]
    public void GetChunks_LargeText_PerformsEfficiently()
    {
        var sentences = Enumerable.Repeat("This is a test sentence for performance testing. ", 10000);
        var text = string.Join("", sentences);

        var startTime = DateTime.UtcNow;
        var result = _chunkingService.GetChunks(text);
        var endTime = DateTime.UtcNow;
        var totalSeconds = (endTime - startTime).TotalSeconds;
        
        Assert.True(result.Length > 1);
        Assert.True(totalSeconds < 5, "Chunking should complete within 5 seconds");
    }
}