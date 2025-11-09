using Npgsql;
using System.Data;
using PaperBuddy.Web.Common.Abstractions;
using PaperBuddy.Web.Features.GetPapers;
using PaperBuddy.Web.Features.UploadPaper;
using PaperBuddy.Web.Infrastructure.Services;

// ReSharper disable CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDbConnection>(sp =>
            new NpgsqlConnection(configuration.GetConnectionString("PostgresConnection")));

        return services;
    }

    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<UploadPaperHandler>();
        services.AddScoped<GetPapersHandler>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmbeddingService, OllamaEmbeddingService>();
        services.AddScoped<ISummarizationService, OllamaSummarizationService>();

        return services;
    }

}