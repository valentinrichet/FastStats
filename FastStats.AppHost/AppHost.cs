using Projects;

var builder = DistributedApplication.CreateBuilder(args);

const string databaseName = "database";

var postgres = builder
    .AddPostgres("postgres")
    .WithEnvironment("POSTGRES_DB", databaseName);

var database = postgres.AddDatabase(databaseName);

var cache = builder
    .AddRedis("redis");

builder.AddProject<FastStats_Api>("api")
    .WithReference(database)
    .WithReference(cache)
    .WaitFor(database)
    .WaitFor(cache);

builder.Build().Run();