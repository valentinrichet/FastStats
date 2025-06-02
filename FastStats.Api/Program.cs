using FastStats.Api.Configurations;
using FastStats.Api.Features.Computations;
using FastStats.Domain;
using FastStats.Infrastructure;
using FastStats.Infrastructure.Persistence;
using FastStats.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddDatabaseInfrastructureServices();
builder.AddCacheInfrastructureServices();
builder.AddDomainServices();
builder.AddApplicationServices();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await app.InitialiseDatabaseAsync(CancellationToken.None);
}

app.UseHttpsRedirection();

app.MapDefaultEndpoints();
app.MapComputationsEndpoints();

app.Run();

public partial class Program
{
}