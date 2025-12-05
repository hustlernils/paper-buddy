using Microsoft.Extensions.Configuration;

namespace PaperBuddy.Web.Tests;

[Collection("TestCollection")]
public abstract class TestBase(TestFixture fixture)
{
    protected IServiceProvider Services => fixture.ServiceProvider;
    private string PdfPath => fixture.Configuration.GetValue<string>("PdfPath")!;
    protected byte[] PdfFile => File.ReadAllBytes(PdfPath);
}