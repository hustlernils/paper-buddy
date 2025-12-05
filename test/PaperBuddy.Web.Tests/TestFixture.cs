using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaperBuddy.Web.Common.Abstractions;
using PaperBuddy.Web.Infrastructure.Services;

namespace PaperBuddy.Web.Tests;

[CollectionDefinition("TestCollection")]
public class TestCollectionFixture : ICollectionFixture<TestFixture>
{
}

public class TestFixture
{
    public IServiceProvider ServiceProvider { get; }
    public IConfiguration Configuration { get; }

    public TestFixture()
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        Configuration = configBuilder.Build();

        var services = new ServiceCollection();
        
        services.AddTransient<IPdfMetadataExtractor, PdfMetadataExtractor>();

        ServiceProvider = services.BuildServiceProvider();
    }
}