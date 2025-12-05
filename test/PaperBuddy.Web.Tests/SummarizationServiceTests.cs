using Microsoft.Extensions.DependencyInjection;
using PaperBuddy.Web.Common.Abstractions;

namespace PaperBuddy.Web.Tests;

[Collection("TestCollection")]
public class SummarizationServiceTests : TestBase
{
    private readonly IPdfMetadataExtractor _pdfMetadataExtractor;
    private readonly ISummarizationService _summarizationService;
    
    public SummarizationServiceTests(TestFixture fixture) : base(fixture)
    {
        _pdfMetadataExtractor = Services.GetRequiredService<IPdfMetadataExtractor>();
        _summarizationService =  Services.GetRequiredService<ISummarizationService>();
    }

    [Fact]
    public async Task Test()
    {
        var text = await _pdfMetadataExtractor.ExtractTextAsync(PdfFile);

        var summary = await _summarizationService.SummarizeAsync(text[0]);

        Assert.NotNull(summary);
    }
}