using System.Text.RegularExpressions;

namespace PaperBuddy.Web.Infrastructure.Services;


public class TextChunk
{
    public int Id { get; set; }
    public string Text { get; set; }
}

public class ChunkingService
{
    private const double TokenSize = 0.75;
    private const int SentenceOverlap = 1;
    private const int ApproximateChunkSize = 500;
    public TextChunk[] GetChunks(string text)
    {
        List<string> chunks = [];
        List<string> buffer = [];
        
        var sentences = Regex.Split(text, @"(?<=[\.!\?])\s+");

        foreach (var sentence in sentences)
        {
            buffer.Add(sentence);

            if (ApproximateTokenCount(buffer) > ApproximateChunkSize)
            {
                AddBufferToChunks(buffer, chunks);
                
                buffer = buffer.TakeLast(SentenceOverlap).ToList();
            }
        }

        if (buffer.Any())
        {
            AddBufferToChunks(buffer, chunks);
        }

        return chunks.Select((text, index) => new TextChunk { Text = text, Id = index}).ToArray();
    }

    private void AddBufferToChunks(List<string> buffer, List<string> chunks)
    {
        chunks.Add(string.Join(" ", buffer));
    }
    
    private int ApproximateTokenCount(string text)
        => text.Length / 4; // because one token corresponds to roughly 4 characters (in english)!
    private int ApproximateTokenCount(List<string> sentences)
        => ApproximateTokenCount(string.Join("", sentences));
}