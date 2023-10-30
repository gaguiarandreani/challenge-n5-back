using SecurityApi.Application;
using SecurityApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Linq;
using SecurityApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    var jsonInputFormatter = options.InputFormatters
        .OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>()
        .Single();

    jsonInputFormatter.SupportedMediaTypes.Add("application/csp-report");
});

builder.Services.AddHealthChecks();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IElasticClient>(s =>
{
    var settings = new ConnectionSettings(new Uri("http://gustavo.com"))
        .DefaultIndex("index-name");

    return new ElasticClient(settings);
});

var app = builder.Build();

app.MapControllers();
app.UseCors();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
