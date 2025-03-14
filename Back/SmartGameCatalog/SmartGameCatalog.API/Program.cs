using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar conexión con PostgreSQL
builder.Services.AddSingleton<Database>();
builder.Services.AddSingleton<UserRepository>();

// Configurar Swagger
builder.Services.AddControllers();
builder.Services.AddSingleton<CategoryRepository>();
builder.Services.AddSingleton<VideoGameRepository>();
builder.Services.AddSingleton<ReviewRepository>();
builder.Services.AddSingleton<RecommendationRepository>();
builder.Services.AddSingleton<PurchaseRepository>();
builder.Services.AddSingleton<AIAnalysisRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartGameCatalog API", Version = "v1" });
});

var app = builder.Build();

// Configuración del pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
