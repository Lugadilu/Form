

using FormAPI.Context;
using FormAPI.Middleware;
using FormAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Form API",
        Version = "v1",
        Description = "Form API for Hotel-Platform",
        Contact = new OpenApiContact
        {
            Name = "Hotelplattform",
            Url = new Uri("https://hotelplatform.io"),
            Email = "info@key-card.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Uncomment and test configurations incrementally
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register ApplicationContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register Repositories
builder.Services.AddScoped<IFormRepository, FormRepository>();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Configure Npgsql to use Newtonsoft.Json for JSON serialization
NpgsqlConnection.GlobalTypeMapper.UseJsonNet();

var app = builder.Build();

/*
// Ensure database is migrated and seeded
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}
*/
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages();

// Use the ExceptionMiddleware
app.UseMiddleware<ExceptionMiddleware>();

/*
if (app.Environment.IsDevelopment()) //in development environment
{
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");    //in non-development environment
}
*/

// Use the ContentTypeMiddleware
app.UseMiddleware<ContentTypeMiddleware>();

// Logging middleware to log each request
app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Handling request: {Path}", context.Request.Path);
    await next.Invoke();
    logger.LogInformation("Finished handling request.");
});



app.UseAuthorization();

app.MapControllers();

app.Run();
