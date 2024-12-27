var builder = DistributedApplication.CreateBuilder(args);

var pgusername = builder.AddParameter("postgres");
var pgpassword = builder.AddParameter("password", true);

var pgsql = builder.AddPostgres("pgsql", pgusername, pgpassword)
    .WithPgAdmin()
    .WithDataVolume("DBLabData1");

var pgdb = pgsql.AddDatabase("pgdb");

var apiService = builder.AddProject<Projects.DBLabAspire_ApiService>("apiservice")
    .WithReference(pgdb)
    .WaitFor(pgdb);

builder.Build().Run();
