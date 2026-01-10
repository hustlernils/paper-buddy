using Dapper;
using Npgsql;
using System.Data;
using PaperBuddy.Web.Common.Abstractions;
using PaperBuddy.Web.Domain;
using PaperBuddy.Web.Features.AddChatMessage;
using PaperBuddy.Web.Features.CreateChat;
using PaperBuddy.Web.Features.CreateProject;
using PaperBuddy.Web.Features.GetChatMessages;
using PaperBuddy.Web.Features.GetChats;
using PaperBuddy.Web.Features.GetPapers;
using PaperBuddy.Web.Features.GetProjects;
using PaperBuddy.Web.Features.UploadPaper;
using PaperBuddy.Web.Infrastructure.Services;

using Infrastructure = PaperBuddy.Web.Infrastructure;

// ReSharper disable CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services,
        IConfiguration configuration)
    {
        SqlMapper.AddTypeHandler(new Infrastructure.EnumTypeHandler<ParentType>());

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
        services.AddScoped<CreateChatHandler>();
        services.AddScoped<GetChatsHandler>();
        services.AddScoped<GetChatMessagesHandler>();
        services.AddScoped<AddChatMessageHandler>();

        return services;
    }

    public static IServiceCollection AddPaperBuddy(this IServiceCollection services)
    {
        services.AddScoped<IEmbeddingService, OllamaEmbeddingService>();
        services.AddScoped<ISummarizationService, OllamaSummarizationService>();
        services.AddScoped<IPdfMetadataExtractor, PdfMetadataExtractor>();
        services.AddScoped<ChatService>();

        return services;
    }

}