using Microsoft.OpenApi.Models;
using Provider.API.Extensions;
using Provider.Application;
using Provider.Infrastructure;
using Provider.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddApplicationServices();
//builder.Services.MigrateDatabase();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Provider.API", Version = "v1" });
});

var app = builder.Build();
app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Provider.API v1"));
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
