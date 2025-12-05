using Microsoft.Extensions.Configuration;

namespace PaperBuddy.Web.Tests;

[Collection("TestCollection")]
public abstract class TestBase(TestFixture fixture)
{
    protected IServiceProvider Services => fixture.ServiceProvider;
    protected string PdfPath => fixture.Configuration.GetValue<string>("PdfPath")!;
}