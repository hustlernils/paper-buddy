using System.Data;

namespace PaperBuddy.Web.Common;

public abstract class TransactionRequestHandler<TRequest, TResponse>(IDbConnection connection) : RequestHandler<TRequest, TResponse>(connection)
{
    public override async Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (Database.State != ConnectionState.Open)
            {
                Database.Open();
            }

            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    var result = await HandleAsync(request, cancellationToken);
                    transaction.Commit();
                    return result;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        catch (Exception ex)
        {
            throw; // wrap in a custom exception later 
        }
        finally
        {
            if (Database.State == ConnectionState.Open)
            {
                Database.Close();
            }
        }
    }
}