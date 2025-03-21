using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Cargar configuración de la base de datos
builder.Services.AddSingleton<Database>();

// 🔹 Registrar Repositorios usando Dapper en lugar de EF Core
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<GameRepository>();
builder.Services.AddSingleton<RatingRepository>();
builder.Services.AddSingleton<GameStatusRepository>();
builder.Services.AddSingleton<RecommendationRepository>();

// 🔹 Agregar controladores con compatibilidad para JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantener nombres originales en JSON
    });

// 🔹 Habilitar Swagger para documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<WeaviateService>();


var app = builder.Build();

// 🔹 Habilitar Swagger solo en entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 Middleware para redirección HTTPS (opcional, pero recomendado)
app.UseHttpsRedirection();

// 🔹 Habilitar CORS (si es necesario)
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// 🔹 Configurar rutas para controladores
app.UseAuthorization();
app.MapControllers();

// 🔹 Iniciar la aplicación
app.Run();
