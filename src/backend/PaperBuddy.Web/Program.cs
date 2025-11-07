using PaperBuddy.Web.Features.UploadPaper;

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddHandlers();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.MapUploadPaperEndpoint();

app.Run();