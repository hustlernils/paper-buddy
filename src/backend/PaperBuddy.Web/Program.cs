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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.MapPost("/papers/upload", async (IFormFile file) =>
{
    // dummy action
    Console.WriteLine(file.FileName);
    
    return Results.Ok(new { message = "File uploaded successfully!" });
})
.DisableAntiforgery(); ;


app.Run();

