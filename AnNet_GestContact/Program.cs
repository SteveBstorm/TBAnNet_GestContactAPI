using AnNet_GestContact.Dal.Services;
using AnNet_GestContact.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using MT.Tools.Database;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("tokenValidation").GetSection("secret").Value)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetSection("tokenValidation").GetSection("issuer").Value,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration.GetSection("tokenValidation").GetSection("audience").Value
        };
    }
    );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminPolicy", policy => policy.RequireRole("admin"));
    options.AddPolicy("userPolicy", policy => policy.RequireRole("user", "admin"));
    options.AddPolicy("auth", policy => policy.RequireAuthenticatedUser());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(o => o.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services)
{
    services.AddControllers().AddXmlDataContractSerializerFormatters();
    services.AddSingleton<Connection>(sp => new Connection(builder.Configuration.GetConnectionString("GestContact"), SqlClientFactory.Instance));
    services.AddSingleton<ContactService>();
    services.AddSingleton<UserService>();
    services.AddSingleton<TokenService>();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}