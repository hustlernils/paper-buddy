using System.Data;
using Dapper;

namespace PaperBuddy.Web.Features.GetProjects;

public class GetProjectsHandler(IDbConnection connection)
{
    private readonly IDbConnection _dbConnection = connection;
    public async Task<IEnumerable<GetProjectsResponse>> HandleAsync(GetProjectsRequest request, CancellationToken  cancellationToken)
    {
        _dbConnection.Open();

        var sql = @"SELECT id, title, description FROM projects";
        var projects = await _dbConnection.QueryAsync<GetProjectsResponse>(sql);

        _dbConnection.Close();

        return projects;
    }
}