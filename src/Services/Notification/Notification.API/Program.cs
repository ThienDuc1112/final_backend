using Microsoft.OpenApi.Models;
using Notification.API.Data;
using Notification.API.Repositories;
using EventBus.Messages.Common;
using MassTransit;
using Notification.API.EventBusConsumer;
using Notification.API.MyHub;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification.API", Version = "v1" });
});

builder.Services.AddScoped<INotificationContext, NotificationContext>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddSignalR();
builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<SendingMessageConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstant.SendingMessageQueue, c =>
        {
            c.ConfigureConsumer<SendingMessageConsumer>(ctx);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification.API v1"));
}

app.UseCors("CORSPolicy");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("/notificationHub");
});
app.MapControllers();

app.Run();
