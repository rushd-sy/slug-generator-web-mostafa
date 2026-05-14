using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using FluentValidation;
using Microsoft.Extensions.Options;
<<<<<<< HEAD
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
=======
using Serilog;
>>>>>>> main
using SlugGeneratorWeb.Configurations;
using SlugGeneratorWeb.DTOs.Requests;
using SlugGeneratorWeb.Middlewares;
using SlugGeneratorWeb.Validation;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

// Adds services for using Problem Details format
builder.Services.AddProblemDetails();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// API versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(2);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddMvc() // This is needed for controllers
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VV";
    options.SubstituteApiVersionInUrl = true;
});

// Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

// Register swagger service and configuration
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
builder.Services.AddSwaggerGen();

// Register fluent validation
builder.Services.AddScoped<IValidator<GenerateSlugRequest>, GenerateSlugValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Register global exception handler middleware
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Converts unhandled exceptions into Problem Details responses
app.UseExceptionHandler();

// Returns the Problem Details response for (empty) non-successful responses
app.UseStatusCodePages();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant()
            );
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();