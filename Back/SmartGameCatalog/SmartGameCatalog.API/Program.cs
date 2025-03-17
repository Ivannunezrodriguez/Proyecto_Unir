using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Repositories;
using Microsoft.OpenApi.Models;
using SmartGameCatalog.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configurar conexión con PostgreSQL
builder.Services.AddSingleton<Database>();

// Inyección de dependencias de los repositorios
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<CategoryRepository>();
builder.Services.AddSingleton<VideoGameRepository>();
builder.Services.AddSingleton<ReviewRepository>();
builder.Services.AddSingleton<RecommendationRepository>();
builder.Services.AddSingleton<PurchaseRepository>();
builder.Services.AddSingleton<AIAnalysisRepository>();
builder.Services.AddSingleton<PlayedGamesRepository>();
builder.Services.AddSingleton<VideoGameGenreRepository>();
builder.Services.AddSingleton<VideoGamePlatformRepository>();





// Configuración de servicios externos con HttpClient
builder.Services.AddHttpClient<IGDBService>();
builder.Services.AddHttpClient<WeaviateService>();

// Habilitar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartGameCatalog API", Version = "v1" });
});

var app = builder.Build();

// Configuración del pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
