using Npgsql;
using System.Data;
using PaperBuddy.Web.Common.Abstractions;
using PaperBuddy.Web.Features.CreateProject;
using PaperBuddy.Web.Features.GetPapers;
using PaperBuddy.Web.Features.GetProjects;
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
        services.AddScoped<CreateProjectHandler>();
        services.AddScoped<GetProjectsHandler>();

        return services;
    }

    public static IServiceCollection AddPaperBuddy(this IServiceCollection services)
    {
        services.AddScoped<IEmbeddingService, OllamaEmbeddingService>();
        services.AddScoped<ISummarizationService, OllamaSummarizationService>();
        services.AddScoped<IPdfMetadataExtractor, PdfMetadataExtractor>();

        return services;
    }

}