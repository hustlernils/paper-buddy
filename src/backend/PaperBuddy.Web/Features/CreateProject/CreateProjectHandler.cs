using System.Data;
using Dapper;
using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.CreateProject;

public class CreateProjectHandler(IDbConnection connection)
{
    private readonly IDbConnection _dbConnection = connection;

    public async Task<Guid> HandleAsync(CreateProjectRequest request, CancellationToken  cancellationToken)
    {
        _dbConnection.Open();

        var project = new Project(new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a"),  request.Title, request.Description);

        await _dbConnection.ExecuteAsync(
            @"INSERT INTO projects (id, user_id, title, description, created_at)
            VALUES (@Id, @UserId, @Title, @Description, NOW())", project);

        _dbConnection.Close();

        return project.Id;
    }
}