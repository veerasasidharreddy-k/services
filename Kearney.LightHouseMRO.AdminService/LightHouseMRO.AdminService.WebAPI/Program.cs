using System.Text;
using Azure.Identity;
using LightHouseMRO.AdminService.Core.Extensions;
using LightHouseMRO.AdminService.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//IdentityModelEventSource.ShowPII = true;
// Load configuration from Azure App Configuration
/*builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(builder.Configuration.GetValue<string>("ConnectionStrings:AppConfig"))
           // Load all keys that start with `AdminService:` and have no label
           .Select("AdminService:*")
           // Configure to reload configuration if the registered key 'AdminService:Configreload' is modified.
           // Use the default cache expiration of 30 seconds. It can be overriden via AzureAppConfigurationRefreshOptions.SetCacheExpiration.
           .ConfigureRefresh(refreshOptions =>
           {
               refreshOptions.Register("AdminService:Configreload", refreshAll: true);
           })
           .ConfigureKeyVault(kv =>
           {
               kv.SetCredential(new ClientSecretCredential("f7804bb5-8d28-4479-a590-f1ac7c4a515f",
                   "0edeaa9c-d8e0-491c-bd2f-02a25d7ab9d8",
                   "8hi7Q~VW3HQsZTSo3OkbSmgx~E_x_VElwihya"));
           });
});*/

// Add services to the container.

// Setting configuration for protected web api



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthentication()
                .AddMicrosoftIdentityWebApi(builder.Configuration, "AzureAd", "AzureAd");


builder.Services
    .AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddAzureAppConfiguration();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigureCoreServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
