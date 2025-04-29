using DotNetEnv;
using UrlShortner.Helpers;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.WebHost.UseUrls("http://0.0.0.0:5149");

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
// builder.Services.AddOpenApi();
builder.Services.AddControllers();

if (Environment.GetEnvironmentVariable("ALLOWED_ORIGINS") == null)
{
    throw new NullReferenceException("No ALLOWED ROUTES found");
}
else
{
    string[] allowedOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")!.Split(',');
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecific", policy =>
        {
            policy.WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
}

if (!string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("MySQLConnection")))
{
    builder.Services.AddSingleton<DapperHelper>(sp =>
    {
        string connectionString = builder.Configuration.GetConnectionString("MySQLConnection")!;
        return new DapperHelper(connectionString);
    });
}

var app = builder.Build();
app.UseCors("AllowSpecific");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Swagger"));
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();