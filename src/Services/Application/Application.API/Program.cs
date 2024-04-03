using Application.API.GrpcServices;
using Application.Infrastructure;
using Business.Grpc.Protos;
using Candidate.Grpc.Protos;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "applicationAPI", Version = "v1" });
});

builder.Services.AddGrpcClient<JobProtoService.JobProtoServiceClient>
               (o => o.Address = new Uri(configuration["GrpcSettings:GrpcJobUrl"]));
builder.Services.AddGrpcClient<MatchingJobProtoService.MatchingJobProtoServiceClient>
               (o => o.Address = new Uri(configuration["GrpcSettings:GrpcJobUrl"]));
builder.Services.AddGrpcClient<ResumeProtoService.ResumeProtoServiceClient>
               (o => o.Address = new Uri(configuration["GrpcSettings:GrpcResumeUrl"]));
builder.Services.AddGrpcClient<MatchingResumeProtoService.MatchingResumeProtoServiceClient>
               (o => o.Address = new Uri(configuration["GrpcSettings:GrpcResumeUrl"]));

builder.Services.AddScoped<JobGrpcService>();
builder.Services.AddScoped<ResumeGrpcService>();
builder.Services.AddScoped<MatchingResumeGrpcService>();
builder.Services.AddScoped<MatchingJobGrpcService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(configuration["EventBusSettings:HostAddress"]);
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost3000");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
