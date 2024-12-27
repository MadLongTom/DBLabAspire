using DBLabAspire.ApiService.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<ApplicationDbContext>("pgdb",options =>{ },settings => { 
    settings.EnableSensitiveDataLogging();
    settings.EnableDetailedErrors();
    settings.UseLazyLoadingProxies();
});
builder.EnrichNpgsqlDbContext<ApplicationDbContext>();
// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHttpLogging();
builder.Services.AddCors(option => option.AddPolicy("any", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
var app = builder.Build();

app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

app.MapOpenApi();
app.MapScalarApiReference();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseHttpLogging();

app.MapControllers();

app.MapDefaultEndpoints();
app.UseCors("any");
app.Run();
