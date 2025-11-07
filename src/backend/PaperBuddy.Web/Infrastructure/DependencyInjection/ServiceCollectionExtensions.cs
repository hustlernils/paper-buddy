using Npgsql;
using System.Data;
using PaperBuddy.Web.Features.UploadPaper;

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

        return services;
    }
}