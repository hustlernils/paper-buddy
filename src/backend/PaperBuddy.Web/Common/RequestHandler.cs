using System.Data;
using Dapper;

namespace PaperBuddy.Web.Common;

public abstract class RequestHandler<TRequest, TResponse>(IDbConnection connection)
{
    protected readonly IDbConnection Database = connection;

    public async Task<TResponse> Execute(TRequest request)
    {
        try
        {
            if (Database.State != ConnectionState.Open)
            {
                Database.Open();
            }

            return await HandleAsync(request)
        }
        finally
        {
            if (Database.State == ConnectionState.Open)
            {
                Database.Close();
            }
        }
    }

    public abstract async Task<TResponse> HandleAsync(TRequest request);
}