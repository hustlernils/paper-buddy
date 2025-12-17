using System.Data;
using Dapper;

namespace PaperBuddy.Web.Common;

public abstract class RequestHandler<TRequest, TResponse>(IDbConnection connection)
{
    protected readonly IDbConnection Database = connection;

    public async Task<TResponse> Execute(TRequest request, CancellationToken  cancellationToken)
    {
        try
        {
            if (Database.State != ConnectionState.Open)
            {
                Database.Open();
            }

            return await HandleAsync(request, cancellationToken);
        }
        finally
        {
            if (Database.State == ConnectionState.Open)
            {
                Database.Close();
            }
        }
    }

    protected abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}