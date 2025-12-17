using PaperBuddy.Web.Features.CreateChat;
using PaperBuddy.Web.Features.GetPapers;
using PaperBuddy.Web.Features.UploadPaper;
using PaperBuddy.Web.Features.GetProjects;
using PaperBuddy.Web.Features.CreateProject;
using PaperBuddy.Web.Features.GetChats;

var builder = WebApplication.CreateBuilder(args);

const string  MyAllowSpecificOrigins = "MyAllowSpecificOrigins";

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  => {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddHandlers();
builder.Services.AddMessageBus(config =>
{
    config.AddConsumer<SummarizePaperHandler>();
    config.AddConsumer<ExtractPaperInfoHandler>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.Services.UseMessageBus();

app.MapUploadPaperEndpoint();
app.MapGetPapersEndpoint();
app.MapGetProjectsEndpoint();
app.MapCreateProjectEndpoint();
app.MapGetChatsEndpoint();
app.MapCreateChatEndpoint();

app.Run();