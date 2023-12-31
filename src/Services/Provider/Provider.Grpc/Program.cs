using Provider.Grpc.Services;
using Provider.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CareerService>();
app.MapGrpcService<LanguageService>();
app.MapGrpcService<SkillService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
