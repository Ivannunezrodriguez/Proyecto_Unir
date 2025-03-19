using Microsoft.OpenApi.Models;
using SmartGameCatalog.API.Services;
using SmartGameCatalog.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar conexión con PostgreSQL
builder.Services.AddDbContext<SmartGameDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de CORS (Permite que el frontend acceda a la API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Inyección de dependencias de los repositorios
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<GameRepository>();
builder.Services.AddScoped<RatingRepository>();
builder.Services.AddScoped<FavoriteRepository>();
builder.Services.AddScoped<RecommendationRepository>();
builder.Services.AddScoped<GameStatusRepository>();

// Configuración de servicios externos con HttpClient
builder.Services.AddHttpClient<IGDBService>();
builder.Services.AddHttpClient<WeaviateService>();

// Servicio para manejar autenticación de IGDB
builder.Services.AddSingleton<IGDBAuthService>();

// Habilitar controladores
builder.Services.AddControllers();

// Configurar Swagger para documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "SmartGameCatalog API", 
        Version = "v1",
        Description = "API para el catálogo inteligente de videojuegos con recomendaciones personalizadas.",
        Contact = new OpenApiContact
        {
            Name = "Soporte SmartGameCatalog",
            Email = "soporte@smartgamecatalog.com"
        }
    });
});

var app = builder.Build();

// Configuración del pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS
app.UseCors("AllowAllOrigins");

// Habilitar redirección HTTPS (Desactívala si solo pruebas en local)
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
