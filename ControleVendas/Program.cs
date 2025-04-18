using ControleVendas.Infra.Config;
using ControleVendas.Infra.Data;
using ControleVendas.Infra.Middlewares;
using ControleVendas.Modules.User.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Configuração das opções JSON usando a classe JsonConfig
builder.Services.AddJsonConfiguration();

// Configuração do rate limiting usando a class RateLimiterConfig
builder.Services.AddRateLimiterGlobal();

// Configuração as politicas do cors usando a class PolicyCorsConfig
builder.Services.AddPolicyCors();

builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger usando a classe SwaggerConfig
builder.Services.AddSwaggerConfiguration(builder.Configuration);

//Configura o Indenty
builder.Services.AddIdentity<ApplicationUserEntity, IdentityRole>()
    .AddEntityFrameworkStores<AppDbConnectionContext>()
    .AddDefaultTokenProviders();

// Configuração do JWT usando a class JwtConfig
builder.Services.AddJwtConfiguration(builder.Configuration); 

// Configuração de Políticas de Autorização usando a class AuthorizationConfig
builder.Services.AddAuthorizationPolicies();

// Config AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
//
// Config conection SGBD
builder.Services.AddDbContext<AppDbConnectionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

// Configuração de injeções de dependência usando a class DependencyInjectionConfig
builder.Services.AddDependencyInjections();


// Configuração do logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseCors();

app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();