using AuthenticationSever.Configuration;
using AuthenticationSever.Data;
using AuthenticationSever.Entities;
using AuthenticationSever.ExtensionGrant;
using AuthenticationSever.Extensions;
using AuthenticationSever.Repositories.Abstract;
using AuthenticationSever.Repositories.Implementation;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


var connectionString = configuration.GetConnectionString("AuthenticationConnectionString");

builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserAuthentication, UserAuthentication>();

builder.Services.AddIdentity<ManageUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

builder.Services.AddScoped<IProfileService, ProfileServiceAuthentication>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryPersistedGrants()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryClients(Config.GetClients())
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddAspNetIdentity<ManageUser>()
    .AddExtensionGrantValidator<ExternalAuthenticationGrant<ManageUser>>();


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = IdentityConstants.ApplicationScheme;
//    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
//})
//    .AddGoogle(googleOptions =>
//    {
//        googleOptions.ClientId = "25722280879-r97hgd7ff9n76u59n7rrimh39ctmrutj.apps.googleusercontent.com";
//        googleOptions.ClientSecret = "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";
//        googleOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
//    });

builder.Services.AddServices<ManageUser>()
                    .AddRepositories()
                    .AddProviders<ManageUser>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost3000");
app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityServer();
app.MapControllers();

app.Run();