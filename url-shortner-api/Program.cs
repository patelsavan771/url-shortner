using UrlShortner.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddControllers();

if (!string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("MySQLConnection")))
{
    builder.Services.AddSingleton<DapperHelper>(sp =>
    {
        string connectionString = builder.Configuration.GetConnectionString("MySQLConnection")!;
        return new DapperHelper(connectionString);
    });
}

var app = builder.Build();
app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Swagger"));
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();