using Microsoft.Extensions.DependencyInjection;
using PaperBuddy.Web.Common.Abstractions;

namespace PaperBuddy.Web.Tests;

[Collection("TestCollection")]
public class PdfMetadataExtractorTests : TestBase
{
    private readonly IPdfMetadataExtractor _pdfMetadataExtractor;
    public PdfMetadataExtractorTests(TestFixture fixture) : base(fixture)
    {
        _pdfMetadataExtractor = Services.GetRequiredService<IPdfMetadataExtractor>();
    }

    [Fact]
    public async Task ExtractMetadataAsync_Returns_Metadata()
    {
        var metadata = await _pdfMetadataExtractor.ExtractMetadataAsync(PdfFile);
        
        Assert.NotNull(metadata);
        Assert.NotNull(metadata.Authors);
        Assert.NotNull(metadata.Title);
        Assert.NotNull(metadata.Year);
    }
    
    [Fact]
    public async Task ExtractTextAsync_Returns_Text()
    {
        var text = await _pdfMetadataExtractor.ExtractTextAsync(PdfFile);
        
        Assert.NotNull(text);
        Assert.NotEmpty(text);
    }
    
    [Fact]
    public async Task ExtractParagraphsAsync_Returns_Text()
    {
        var text = await _pdfMetadataExtractor.ExtractParagraphsAsync(PdfFile);
        
        Assert.NotNull(text);
        Assert.NotEmpty(text);
    }
}