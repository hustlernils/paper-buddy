using Microsoft.Extensions.DependencyInjection;
using PaperBuddy.Web.Common.Abstractions;

namespace PaperBuddy.Web.Tests;

[Collection("TestCollection")]
public class PdfMetadataExtractorTests : TestBase
{
    private readonly byte[] _pdfFile;
    private readonly IPdfMetadataExtractor _pdfMetadataExtractor;
    public PdfMetadataExtractorTests(TestFixture fixture) : base(fixture)
    {
        _pdfFile = File.ReadAllBytes(PdfPath);
        _pdfMetadataExtractor = Services.GetRequiredService<IPdfMetadataExtractor>();
    }

    [Fact]
    public async Task Extractor_Returns_Metadata()
    {
        var metadata = await _pdfMetadataExtractor.ExtractMetadataAsync(_pdfFile);
        
        Assert.NotNull(metadata);
        Assert.NotNull(metadata.Authors);
        Assert.NotNull(metadata.Title);
        Assert.NotNull(metadata.Year);
    }
}